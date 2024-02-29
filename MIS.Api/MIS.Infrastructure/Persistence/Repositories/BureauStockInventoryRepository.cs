using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.BureauStockInventoryFeatures.Commands;
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
    public class BureauStockInventoryRepository : Repository<BureauStockInventory>, IBureauStockInventoryRepository
    {
        public BureauStockInventoryRepository(MISDbContext context) : base(context)
        {
        }

        public async Task<BureauStockInventoryResponse> PostBureauEquipmentAssignment(PostBureauStockInventoryRequest request)
        {

            var qrCode = await context.QrCodes.FirstOrDefaultAsync(q => q.Designation.Equals(request.QrCode));
            if (qrCode == null)
                return new BureauStockInventoryResponse()
                {
                    Code = 0,
                    Key = "Error",
                    Value = "Qr code non existant"
                };
            string prefix = new string(request.QrCode.Where(char.IsLetter).ToArray());
            var bureau = await context.Bureaus.FirstOrDefaultAsync(b => b.Abbreviation == prefix);
            if (bureau == null)
                return new BureauStockInventoryResponse()
                {
                    Code = 3,
                    Key = "Error",
                    Value = "Il y a aucun bureau avec l'abbreviation ' " + prefix + " '"
                };
            var assigedCode = await context.BureauStockInventories.FirstOrDefaultAsync(a => a.QrCodeId == qrCode.Id && a.IsAvailable == false);
            var assigedStock = await context.BureauStockInventories.FirstOrDefaultAsync(a => a.StockId == request.StockId && a.IsAvailable == false);
            if (assigedCode != null)
                return new BureauStockInventoryResponse()
                {
                    Code = 1,
                    Key = "Error",
                    Value = "Qr code déjà affecté"
                };

            if (assigedStock != null)
                return new BureauStockInventoryResponse()
                {
                    Code = 4,
                    Key = "Error",
                    Value = "L'equipment est déjà affecté"
                };
            var state = request.State == 0 ? Domain.Enums.EquipmentState.EXCELLENT : (request.State == 1 ? Domain.Enums.EquipmentState.GOOD : Domain.Enums.EquipmentState.DAMAGED);
            var assignment = new BureauStockInventory()
            {
                QrCodeId = qrCode.Id,
                BureauId = bureau.Id,
                StockId = request.StockId,
                UserId = request.UserId,
                InventoryDate = request.AssignmentDate,
                EquipmentState = state,
                IsAvailable = false
            };
            await context.BureauStockInventories.AddAsync(assignment);
            if (await context.SaveChangesAsync() > 0)
                return new BureauStockInventoryResponse()
                {
                    Code = 2,
                    Key = "Success",
                    Value = ""
                };
            else
            {
                return new BureauStockInventoryResponse()
                {
                    Code = 3,
                    Key = "Error",
                    Value = ""
                };
            }
        }

        public async Task<InventoryHistoryResponse> GetBureauEquipmentAssignmentHistory(BureauStockInventoryFilterRequest request)
        {
            //request.PageSize = 5;

            var data = await context.BureauStockInventories
                .Include(a => a.QrCode)
                .Include(a => a.Stock)
                .Include(a => a.Stock.Equipment)
                .Include(a => a.Bureau.Service)
                .Include(a => a.Bureau.Service.Division)
                .Include(a => a.Supplier)
                .Include(a => a.Supplier.SupplierType)
                .OrderByDescending(a => a.InventoryDate)
                .Where(a => a.DeletedAt == null && a.IsAvailable == false)
                .ToListAsync();

            var result = data;

            var paginatedResult = new List<BureauStockInventory>();

            if (request.QrCodeId != null && request.QrCodeId.HasValue && request.QrCodeId != Guid.Empty)
            {
                result = result.Where(r => r.QrCodeId == request.QrCodeId).ToList();
            }
            if (request.DivisionId != null && request.DivisionId.HasValue && request.DivisionId != Guid.Empty)
            {
                result = result.Where(r => r.Bureau.Service.DivisionId == request.DivisionId).ToList();
            }
            if (request.ServiceId != null && request.ServiceId.HasValue && request.ServiceId != Guid.Empty)
            {
                result = result.Where(r => r.Bureau.ServiceId == request.ServiceId).ToList();
            }
            if (request.BureauId != null && request.BureauId.HasValue && request.BureauId != Guid.Empty)
            {
                result = result.Where(r => r.BureauId == request.BureauId).ToList();
            }
            if (request.EquipmentId != null && request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                result = result.Where(r => r.Stock.EquipmentId == request.EquipmentId).ToList();
            }
            if (request.StockId != null && request.StockId.HasValue && request.StockId != Guid.Empty)
            {
                result = result.Where(r => r.StockId == request.StockId).ToList();
            }
            if (request.SupplierTypeId != null && request.SupplierTypeId.HasValue && request.SupplierTypeId != Guid.Empty)
            {
                result = result.Where(r => r.Supplier.SupplierTypeId == request.SupplierTypeId).ToList();
            }
            if (request.SupplierId != null && request.SupplierId.HasValue && request.SupplierId != Guid.Empty)
            {
                result = result.Where(r => r.SupplierId == request.SupplierId).ToList();
            }

            int count = 0;

            paginatedResult = result.Skip((request.Page ?? 0) * request.PageSize.Value)
                .Take(request.PageSize.Value).ToList();

            count = result.Count;

            return new InventoryHistoryResponse()
            {
                BureauStockInventoryHistoryResponses = paginatedResult.Select(r => new BureauStockInventoryHistoryResponse
                {
                    Id = r.Id,
                    QrCode = r.QrCode?.Designation,
                    DivisionName = r.Bureau?.Service?.Division?.Name,
                    ServiceName = r.Bureau?.Service?.Name,
                    BureauName = r.Bureau?.Name,
                    EquipmentName = r.Stock?.Equipment?.Name,
                    StockDesignation = r.Stock.Designation,
                    SupplierType = r.Supplier?.SupplierType?.Name,
                    Supplier = r.Supplier?.CompanyName,
                    Designation = r.Designation,
                    MarketReference = r.MarketReference,
                    MarketObject = r.MarketObject,
                    State = r.EquipmentState == 0 ? EquipmentsState.EXCELLENT : (((int)r.EquipmentState == 1) ? EquipmentsState.GOOD : EquipmentsState.DAMAGED),
                    IsAvailable = r.IsAvailable,
                }).ToList(),
                Count = count
            };
        }

        public async Task<List<InventoriedStockCountResponse>> GeAssignedEquipmentsCount(BureauStockInventoryFilterRequest request)
        {
            var data = await context.BureauStockInventories
                .Include(a => a.Stock)
                .Include(a => a.Bureau.Service)
                .Include(a => a.Bureau.Service.Division)
                .Where(a => a.IsAvailable == false && a.DeletedAt == null)
                .ToListAsync();



            if (request.DivisionId != null)
            {
                data = data.Where(r => r.Bureau.Service.DivisionId == request.DivisionId).ToList();
            }
            if (request.ServiceId != null)
            {
                data = data.Where(r => r.Bureau.ServiceId == request.ServiceId).ToList();
            }
            if (request.BureauId != null)
            {
                data = data.Where(r => r.BureauId == request.BureauId).ToList();
            }
            if (request.StockId != null)
            {
                data = data.Where(r => r.StockId == request.StockId).ToList();
            }

            var result = data.GroupBy(d => d.Stock.Designation)
                .Select(g => new InventoriedStockCountResponse { Designation = g.Key, Count = g.Count() })
                .ToList();

            return result;
        }

        public async Task<int> UpdateBureauEquipmentAssignmentsAvailability(List<UpdateInventoryAvailabilityRequest> request)
        {
            var count = 0;
            foreach (var item in request)
            {
                var existedAssignment = await context.BureauStockInventories.FirstOrDefaultAsync(a => a.Id == item.Id);
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

        public async Task<BureauStockInventoryResponse> PostInventoryForNewEquipments(PostInventoryForNewEquipmentsRequest request)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var existingQrCode = await context.QrCodes.FirstOrDefaultAsync(q => q.Designation.Equals(request.QrCode));
                if (existingQrCode != null)
                    return new BureauStockInventoryResponse()
                    {
                        Code = 0,
                        Key = "Error",
                        Value = "Qr code déja existe"
                    };
                QrCode qrCode = new()
                {
                    Designation = request.QrCode
                };
                await context.QrCodes.AddAsync(qrCode);
                var assigedStock = await context.BureauStockInventories.FirstOrDefaultAsync(a => a.StockId == request.StockId && a.Stock.IsMultiple && a.IsAvailable == false);
                if (assigedStock != null)
                    return new BureauStockInventoryResponse()
                    {
                        Code = 1,
                        Key = "Error",
                        Value = "L'equipment est déjà affecté"
                    };
                var state = request.State == 0 ? Domain.Enums.EquipmentState.EXCELLENT : (request.State == 1 ? Domain.Enums.EquipmentState.GOOD : Domain.Enums.EquipmentState.DAMAGED);
                var assignment = new BureauStockInventory()
                {
                    QrCodeId = qrCode.Id,
                    BureauId = request.BureauId,
                    StockId = request.StockId,
                    UserId = request.UserId,
                    InventoryDate = DateTime.Now,
                    EquipmentState = state,
                    IsAvailable = false,
                    Designation = request.Designation,
                    MarketReference = request.MarketReference,
                    MarketObject = request.MarketObject,
                    SupplierId = request.SupplierId,
                };
                await context.BureauStockInventories.AddAsync(assignment);
                if (await context.SaveChangesAsync() > 0)
                {
                    await transaction.CommitAsync();
                    return new BureauStockInventoryResponse()
                    {
                        Code = 2,
                        Key = "Success",
                        Value = ""
                    };
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new BureauStockInventoryResponse()
                    {
                        Code = 3,
                        Key = "Error",
                        Value = "Entities not saved"
                    };
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new BureauStockInventoryResponse()
                {
                    Code = 4,
                    Key = "Error",
                    Value = "Transaction failed"
                };
            }

        }
    }
}
