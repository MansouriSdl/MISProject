using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.BureauStockAssignmentFeatures.Commands;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Constants;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class BureauStockAssignmentRepository : Repository<BureauStockAssignment>, IBureauStockAssignmentRepository
    {
        public BureauStockAssignmentRepository(MISDbContext context) : base(context)
        {
        }

        public async Task<MessageResponse> AddAssignment(AddAssignmentRequest request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                var stock = await context.Stocks.FirstOrDefaultAsync(s => s.Id == request.StockId);
                if(stock == null)
                {
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
                bool isAvailable = true;
                if (stock.IsMultiple)
                {
                    var existingAssignment = await context.BureauStockAssignments.FirstOrDefaultAsync(a => a.StockId == request.StockId && a.DeletedAt == null && a.IsAvailable == false);
                    if(existingAssignment != null)
                    {
                        isAvailable = false;
                        return new MessageResponse()
                        {
                            StatusCode = 400,
                            Message = "Bad Request"
                        };
                    }

                }
                else
                {
                    
                    if(stock.Quantity <= 0)
                    {
                        isAvailable = false;
                        return new MessageResponse()
                        {
                            StatusCode = 400,
                            Message = "Bad Request"
                        };
                    }
                    else if(stock.Quantity - request.Quantity < 0)
                    {
                        isAvailable = false;
                        return new MessageResponse()
                        {
                            StatusCode = 400,
                            Message = "Bad Request"
                        };
                    }
                    else
                    {
                        var stockToUpdateQuantity = await context.Stocks.FirstOrDefaultAsync(s => s.Id == request.StockId && s.DeletedAt == null);
                        if(stockToUpdateQuantity == null)
                        {
                            isAvailable = false;
                            return new MessageResponse()
                            {
                                StatusCode = 400,
                                Message = "Bad Request"
                            };
                        }
                        else
                        {
                            isAvailable = true;
                            stockToUpdateQuantity.Quantity -= request.Quantity;
                            context.Entry(stockToUpdateQuantity).State = EntityState.Modified;
                        }
                    }
                }
                if(isAvailable == true)
                {
                    BureauStockAssignment bureauStockAssignment = new()
                    {
                        BureauId = request.BureauId,
                        StockId = request.StockId,
                        Quantity =(stock.IsMultiple) ? 1 : request.Quantity,
                        AssignmentDate = DateTime.Now,
                        IsAvailable = false,
                        UserId = request.UserId
                    };
                    await context.BureauStockAssignments.AddAsync(bureauStockAssignment);
                    if (await context.SaveChangesAsync() > 0)
                    {
                        transaction.Commit();
                        return new MessageResponse()
                        {
                            StatusCode = 200,
                            Message = "Success"
                        };
                    }
                    else
                    {
                        transaction.Rollback();
                        return new MessageResponse()
                        {
                            StatusCode = 400,
                            Message = "Bad Request"
                        };
                    }

                }
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
        }

        public async Task<List<BureauStockAssignmentsResponse>> GeBureauStockAssignments(FilterRequest request)
        {
            var assignments = await context.BureauStockAssignments
                .Include(a => a.Bureau)
                .Include(a => a.Bureau.Service)
                .Include(a => a.Stock)
                .Include(a => a.Stock.Equipment)
                .Include(a => a.User)
                .Where(a => a.DeletedAt == null && a.IsAvailable == false)
                .OrderByDescending(a => a.AssignmentDate)
                .ToListAsync();
            if (request.DivisionId.HasValue && request.DivisionId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.Service.DivisionId == request.DivisionId).ToList();
            }
            if (request.ServiceId.HasValue && request.ServiceId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.ServiceId == request.ServiceId).ToList();
            }
            if (request.BureauId.HasValue && request.BureauId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.BureauId == request.BureauId).ToList();
            }
            if (request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Stock.EquipmentId == request.EquipmentId).ToList();
            }
            if (request.StockId.HasValue && request.StockId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.StockId == request.StockId).ToList();
            }
            if (request.Date.HasValue)
            {
                assignments = assignments.Where(o => o.AssignmentDate.Year == request.Date.Value.Year && o.AssignmentDate.Month == request.Date.Value.Month && o.AssignmentDate.Day == request.Date.Value.Day).ToList();
            }
            return assignments.Select(a => new BureauStockAssignmentsResponse()
            {
                Id = a.Id,
                AssignmentDate = a.AssignmentDate.ToShortDateString(),
                BureauId = a.BureauId,
                Bureau = a.Bureau?.Name,
                StockId = a.StockId,
                Stock = a.Stock?.Designation,
                Equipment = a.Stock?.Equipment?.Name,
                Quantity = (int) a.Quantity,
                User = a.User?.LastName + " " + a.User?.FirstName
            }).ToList();
        }

      

        public async Task<int> UpdateBureauStockAssignmentsAvailability(UpdateBureauStockAssignmentAvailabilityRequest request)
        {
            var count = 0;
            foreach (var item in request.Ids)
            {
                var existedAssignment = await context.BureauStockAssignments.FirstOrDefaultAsync(a => a.Id == item);
                if (existedAssignment != null)
                {
                    existedAssignment.IsAvailable = true;
                    context.Entry(existedAssignment).State = EntityState.Modified;
                    count++;
                }
                else
                {
                    continue;
                }
            }
            await context.SaveChangesAsync();
            return count;
        }

        public async Task<StockAssignmentsDetailsResponse> GetStockAssignmentsDetails(FilterRequest request)
        {
            DateTime today = DateTime.Today;
            if(!request.EquipmentId.HasValue && request.EquipmentId == Guid.Empty)
            {
                return null;
            }
            var equipmentName = await context.Equipments
                    .Where(e => e.Id == request.EquipmentId && e.DeletedAt == null)
                    .Select(e => e.Name)
                    .FirstOrDefaultAsync();
            var firstdayofcurrentmonth = new DateTime(today.Year, today.Month, 1);
            var lastdayofcurrentmonth = firstdayofcurrentmonth.AddMonths(1).AddDays(-1);
            var assignments = await context.BureauStockAssignments
                .Include(a => a.Bureau)
                .Include(a => a.Bureau.Service)
                .Include(a => a.Bureau.Service.Division)
                .Include(a => a.Stock)
                .Where(a => a.Stock.EquipmentId == request.EquipmentId && a.DeletedAt == null && a.AssignmentDate.Day >= firstdayofcurrentmonth.Day && a.AssignmentDate.Day <= lastdayofcurrentmonth.Day && a.AssignmentDate.Month == today.Month && a.AssignmentDate.Year == today.Year)
                .Distinct()
                .ToListAsync();

            if(request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Stock.EquipmentId == request.EquipmentId).ToList();
            }
            if (request.DivisionId.HasValue && request.DivisionId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.Service.DivisionId == request.DivisionId).ToList();
            }
            if (request.ServiceId.HasValue && request.ServiceId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.ServiceId == request.ServiceId).ToList();
            }
            if (request.StartDate.HasValue && request.StartDate != null)
            {
                assignments = assignments.Where(a => a.AssignmentDate.Day >= request.StartDate.Value.Day && a.AssignmentDate.Month >= request.StartDate.Value.Month && a.AssignmentDate.Year >= request.StartDate.Value.Year).ToList();
            }
            if (request.EndDate.HasValue && request.EndDate != null)
            {
                assignments = assignments.Where(a => a.AssignmentDate.Day <= request.EndDate.Value.Day && a.AssignmentDate.Month <= request.EndDate.Value.Month && a.AssignmentDate.Year <= request.EndDate.Value.Year).ToList();
            }
            var groupedresult = assignments.GroupBy(g => g.BureauId);
            List<DetailResponse> cardsResponses = groupedresult.Select(g => new DetailResponse()
            {
                Name = context.Bureaus.FirstOrDefault(b => b.Id == g.Key)?.Name,
                Value = g.Sum(q => q.Quantity).Value
            }).ToList();

            var listResponses = assignments
            .GroupBy(entity => new { entity.BureauId, entity.AssignmentDate.Date })
            .OrderBy(group => group.Key.Date)
            .ThenBy(group => group.Key.BureauId)
            .Select(g => new DetailResponse
            {
                Date = g.Key.Date.ToShortDateString(),
                Name = context.Bureaus.FirstOrDefault(b => b.Id == g.First().BureauId)?.Name,
                Value = g.Sum(a => a.Quantity).Value
            })
            .ToList();

            return new StockAssignmentsDetailsResponse()
            {
                Equipment = new NameResponse() { Id = request.EquipmentId.Value, Name = equipmentName },
                CardsResponses = cardsResponses,
                ListResponses = listResponses
            };

        }
    }
}
