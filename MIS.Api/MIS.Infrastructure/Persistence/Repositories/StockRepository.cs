using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.StockFeatures.Commands;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(MISDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            return await context.Stocks.Where(s => s.DeletedAt == null).ToListAsync();
        }

        public async Task<List<StockResponse>> GetStockList(FilterRequest request)
        {
            var stocks = await context.Stocks
                .Include(s => s.Equipment)
                .Include(s => s.Equipment.Orders)
                .Where(s => s.DeletedAt == null)
                .ToListAsync();
            List<Stock> unassignedStocks = new();
            foreach (var stock in stocks)
            {
                var assignment = await context.BureauStockAssignments.FirstOrDefaultAsync(a => a.StockId == stock.Id && a.IsAvailable == false && a.DeletedAt == null);
                if (assignment == null)
                    unassignedStocks.Add(stock);
                else
                    continue;
            }
            if (request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                unassignedStocks = unassignedStocks.Where(s => s.EquipmentId == request.EquipmentId).ToList();
            }

            var groupedStocks = unassignedStocks.GroupBy(s => s.EquipmentId);

            var stockResponses = groupedStocks.Select(group => new StockResponse()
            {
                StockId = stocks.FirstOrDefault(s => s.EquipmentId == group.Key) != null ? stocks.FirstOrDefault(s => s.EquipmentId == group.Key).Id : Guid.Empty,
                EquipmentId = group.Key,
                Designation = stocks.FirstOrDefault(s => s.EquipmentId == group.Key) != null ? stocks.FirstOrDefault(s => s.EquipmentId == group.Key).Designation : null,
                Quantity = group.Sum(s => s.Quantity),
                NextDeliveryQuantity = context.ServiceEquipmentRequests.Where(r => r.EquipmentId == group.Key && r.Status == Domain.Enums.RequestStatus.ORDERED && r.DeletedAt == null && r.OrderId == null).Sum(r => r.Quantity) + context.Orders.Include(o => o.ServiceEquipmentRequest).Where(o => o.DeletedAt == null && o.EquipmentId == group.Key && o.Status == Domain.Enums.OrderStatus.ORDERED && o.ServiceEquipmentRequest == null).Sum(o => o.Quantity).Value,
                Detail = null
            }).ToList(); 

            return stockResponses;

        }

        public async Task<IEnumerable<NameResponse>> GetStocksByEquipmentId(Guid equipmentId)
        {
            var stocks = await context.Stocks.Where(s => s.EquipmentId == equipmentId && s.DeletedAt == null).ToListAsync();
            return stocks.Select(s => new NameResponse()
            {
                Id = s.Id,
                Name = s.Designation
            });
        }

        public async Task<MessageResponse> AddStock(AddStockWithoutOrderRequest request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                if (request.IsMultiple)
                {
                    List<Stock> stocks = new();
                    for (int i = 0; i < request.Quantity; i++)
                    {
                        Stock stock = new()
                        {
                            EquipmentId = request.EquipmentId,
                            Designation = request.Designation + (i+1),
                            Quantity = 1,
                            UserId = request.UserId,
                            IsMultiple = request.IsMultiple,
                            ExpirationDate = request.ExpirationDate ?? null
                        };
                        stocks.Add(stock);
                    }

                    await context.Stocks.AddRangeAsync(stocks);
                }
                else
                {
                    var existingStock = await context.Stocks.FirstOrDefaultAsync(s => s.DeletedAt == null && s.EquipmentId == request.EquipmentId);
                    if(existingStock != null)
                    {
                        existingStock.Quantity += request.Quantity;
                        context.Entry(existingStock).State = EntityState.Modified;
                    }
                    else
                    {
                        Stock stock = new()
                        {
                            EquipmentId = request.EquipmentId,
                            Designation = request.Designation,
                            Quantity = request.Quantity,
                            UserId = request.UserId,
                            IsMultiple = request.IsMultiple,
                            ExpirationDate = request.ExpirationDate ?? null
                        };
                        await context.Stocks.AddAsync(stock);
                    }
                    
                }
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

        public async Task<MessageResponse> AddStockList(List<PostStocksListRequest> request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (var item in request)
                {
                    if (item.IsMultiple)
                    {
                        List<Stock> stocks = new();
                        for (int i = 0; i < item.Quantity; i++)
                        {
                            Stock stock = new()
                            {
                                EquipmentId = item.EquipmentId,
                                Designation = item.Designation,
                                Quantity = 1,
                                UserId = item.UserId,
                                ExpirationDate = item.ExpirationDate ?? null
                            };
                            stocks.Add(stock);
                        }

                        await context.Stocks.AddRangeAsync(stocks);
                    }
                    else
                    {
                        var existingStock = await context.Stocks.FirstOrDefaultAsync(s => s.DeletedAt == null && s.EquipmentId == item.EquipmentId);
                        if (existingStock != null)
                        {
                            existingStock.Quantity += item.Quantity;
                            context.Entry(existingStock).State = EntityState.Modified;
                        }
                        else
                        {
                            Stock stock = new()
                            {
                                EquipmentId = item.EquipmentId,
                                Designation = item.Designation,
                                Quantity = item.Quantity,
                                UserId = item.UserId,
                                ExpirationDate = item.ExpirationDate ?? null
                            };
                            await context.Stocks.AddAsync(stock);
                        }

                    }

                    var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == item.OrderId);
                    if(order != null)
                    {
                        order.Status = Domain.Enums.OrderStatus.IN_STOCK;
                        context.Entry(order).State = EntityState.Modified;
                        var divisionRequest = await context.ServiceEquipmentRequests.FirstOrDefaultAsync(r => r.OrderId == item.OrderId);
                        if(divisionRequest != null)
                        {
                            divisionRequest.Status = Domain.Enums.RequestStatus.IN_STOCK;
                            context.Entry(divisionRequest).State = EntityState.Modified;
                        }
                    }
                   
                }
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

        //charts

        public async Task<List<NameValueResponse>> TopFiveConsummableEquipments(FilterRequest request)
        {
            DateTime today = DateTime.Today;
            var firstdayofcurrentmonth = new DateTime(today.Year, today.Month, 1);
            var lastdayofcurrentmonth = firstdayofcurrentmonth.AddMonths(1).AddDays(-1);
            var assignments = await context.BureauStockAssignments
                .Include(a => a.Stock)
                .Include(a => a.Stock.Equipment)
                .Include(a => a.Bureau)
                .Include(a => a.Bureau.Service)
                .Include(a => a.Bureau.Service.Division)
                .Where(a => a.DeletedAt == null && a.AssignmentDate.Year == today.Year && a.AssignmentDate.Month == today.Month && a.AssignmentDate.Day >= firstdayofcurrentmonth.Day && a.AssignmentDate.Day <= lastdayofcurrentmonth.Day)
                .ToListAsync();
            if(request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Stock.EquipmentId == request.EquipmentId).ToList();
            }
            if(request.BureauId.HasValue && request.BureauId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.BureauId == request.BureauId).ToList();
            }
            if (request.ServiceId.HasValue && request.ServiceId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.ServiceId == request.ServiceId).ToList();
            }
            if (request.DivisionId.HasValue && request.DivisionId != Guid.Empty)
            {
                assignments = assignments.Where(a => a.Bureau.Service.DivisionId == request.DivisionId).ToList();
            }
            if(request.StartDate.HasValue && request.StartDate != null)
            {
                assignments = assignments.Where(a => a.AssignmentDate.Year >= request.StartDate.Value.Year && a.AssignmentDate.Month >= request.StartDate.Value.Month && a.AssignmentDate.Day >= request.StartDate.Value.Day).ToList();
            }
            if (request.EndDate.HasValue && request.EndDate != null)
            {
                assignments = assignments.Where(a => a.AssignmentDate.Year <= request.EndDate.Value.Year && a.AssignmentDate.Month <= request.EndDate.Value.Month && a.AssignmentDate.Day <= request.EndDate.Value.Day).ToList();
            }
            var result = assignments.GroupBy(g => g.Stock.EquipmentId)
                                    .Select(group => new NameValueResponse
                                    {
                                        Name = context.Equipments.FirstOrDefault(e => e.Id == group.Key).Name,
                                        Value = (double) group.Sum(a => a.Quantity) 
                                    })
                                    .OrderByDescending(group => group.Value)
                                    .Take(5)
                                    .ToList();

            return result;
        }
        

        //Alert
        public async Task<AlertResponse> GetAlerts()
        {
            var stocksNotMultiple = await context.Stocks.Include(s => s.Equipment).Where(s => s.IsMultiple == false).ToListAsync();
            var availableStocks = await context.Stocks.Include(s => s.Equipment).Include(s => s.BureauStockAssignments).Where(s => s.IsMultiple && s.BureauStockAssignments.Any(a => a.IsAvailable)).ToListAsync();
            List<Stock> stockList = new();
            stockList.AddRange(stocksNotMultiple);
            stockList.AddRange(availableStocks);
            var groupedStock = stockList.GroupBy(s => s.EquipmentId).ToList();
            DateTime today = DateTime.Today;

            List<NameValueResponse> quantityLimitAlerts = new();
            List<NameValueResponse> expirationDateLimitAlerts = new();

            foreach (var item in groupedStock)
            {
                var equipment = await context.Equipments.FirstOrDefaultAsync(e => e.Id == item.Key);
                var itemQuantity = item.Sum(i => i.Quantity);
                var itemQuantityLimit = equipment?.StockLimit;
                
                if (itemQuantity <= itemQuantityLimit)
                {
                    quantityLimitAlerts.Add(new NameValueResponse()
                    {
                        Name = equipment?.Name,
                        Value = itemQuantity
                    });
                }
                foreach(var i in item)
                {
                    var itemExpirationDate = i.ExpirationDate;
                    if (itemExpirationDate.HasValue)
                    {
                        var alertDays = (equipment.ExpirationDateLimit.HasValue) ? equipment?.ExpirationDateLimit.Value : null;
                        int remainingDays = (int)(itemExpirationDate.Value - today).TotalDays;

                        if (remainingDays <= alertDays)
                        {
                            expirationDateLimitAlerts.Add(new NameValueResponse()
                            {
                                Name = i.Designation,
                                Value = remainingDays
                            });
                        }
                    }
                }
                
                
            }
            return new AlertResponse()
            {
                QuantityLimitAlerts = quantityLimitAlerts,
                ExpirationDateLimitAlerts = expirationDateLimitAlerts
            };
        }
    }
}
