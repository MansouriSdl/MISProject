using Microsoft.EntityFrameworkCore;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : IReport
    {

        MISDbContext mISDbContext;

        public ReportRepository(MISDbContext mISDbContext)
        {
            this.mISDbContext = mISDbContext;
        }

        public async Task<IEnumerable<InventoryLevelReportModel>> GenerateCurrentInventoryLevelsReport()
        {
        var inventoryLevels = await mISDbContext.BureauStockInventories
       .Include(b => b.Stock)
       .ThenInclude(s => s.Equipment)
       .Where(b => b.IsAvailable) // Assuming you want to count only available items
       .GroupBy(b => b.Stock.EquipmentId)
       .Select(group => new InventoryLevelReportModel
       {
           EquipmentId = group.Key,
           EquipmentName = group.First().Stock.Equipment.Name,
           TotalQuantity = group.Sum(g => g.Stock.Quantity)
       })
       .ToListAsync();
        return inventoryLevels;
        }

        public async Task<IEnumerable<EquipmentUsageReportModel>> GenerateEquipmentUsageReportAsync(PredicateDate predicateDate)
        {

         var report = await mISDbContext.Stocks
        .Include(s => s.Equipment)
        .Where(s => s.CreatedAt >= predicateDate.StartDate && s.CreatedAt <= predicateDate.EndDate) // Filter by the specified year
        .GroupBy(s => new { s.EquipmentId, s.Equipment.Name })
        .Select(group => new EquipmentUsageReportModel
        {
            EquipmentId = group.Key.EquipmentId,
            EquipmentName = group.Key.Name,
            TotalQuantity = group.Sum(s => s.Quantity)
        })
        .OrderByDescending(r => r.TotalQuantity)
        .ToListAsync();

            return report;
        }
        public async Task<IEnumerable<MonthlyOrderReportModel>> GenerateMonthlyOrderReportAsync(int year)
        {
            var orders = await mISDbContext.Orders
            .Where(o => o.CreatedAt.Year == year)
            .GroupBy(o => o.CreatedAt.Month)
            .Select(group => new MonthlyOrderReportModel
            {
                Month = group.Key,
                NumberOrders = group.Count()
            })
            .OrderBy(result => result.Month)
            .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<MostConsumedEquipmentModel>> GenerateMostConsumedEquipmentReport()
        {
            var mostConsumed = await mISDbContext.BureauStockInventories
       .Include(i => i.Stock)
       .ThenInclude(s => s.Equipment)
       .Where(i => !i.IsAvailable) // Focus on items that are not available
       .GroupBy(i => i.Stock.EquipmentId)
       .Select(group => new MostConsumedEquipmentModel
       {
           EquipmentId = group.Key,
           EquipmentName = group.First().Stock.Equipment.Name, // Assuming 'Designation' is a meaningful identifier
            ConsumptionRate = group.Count() // Count of times this equipment has been marked as not available
        })
       .OrderByDescending(m => m.ConsumptionRate) // Order by consumption rate to find the most consumed
       .ToListAsync();

            return mostConsumed;
        }

        public async Task<IEnumerable<OrderStatusReportModel>> GenerateOrderStatusReportAsync(PredicateDate predicateDate)
        {

            if (predicateDate.StartDate.HasValue && predicateDate.EndDate.HasValue)
            {

                var totalOrders = await mISDbContext.Orders.Where(s => s.CreatedAt >= predicateDate.StartDate && s.CreatedAt <= predicateDate.EndDate).CountAsync();
                var ordersByStatus = await mISDbContext.Orders
                    .GroupBy(o => o.Status)
                    .Select(group => new
                    {
                        Status = group.Key.ToString(), // Assuming Status is an enum and you want the string representation
                    Count = group.Count()
                    })
                    .ToListAsync();

                var report = ordersByStatus.Select(os => new OrderStatusReportModel
                {
                    Status = os.Status,
                    Value = Math.Round((decimal)os.Count / totalOrders * 100, 2) // Calculate percentage and round to 2 decimal places
                }).ToList();

                return report;


            }
            else
            {
                var totalOrders = await mISDbContext.Orders.Where(s => s.CreatedAt.Year==DateTime.Now.Year).CountAsync();
                var ordersByStatus = await mISDbContext.Orders
                  .GroupBy(o => o.Status)
                  .Select(group => new
                  {
                      Status = group.Key.ToString(), // Assuming Status is an enum and you want the string representation
                        Count = group.Count()
                  })
                  .ToListAsync();

                var report = ordersByStatus.Select(os => new OrderStatusReportModel
                {
                    Status = os.Status,
                    Value = Math.Round((decimal)os.Count / totalOrders * 100, 2) // Calculate percentage and round to 2 decimal places
                }).ToList();
                return report;
            }

        }
    }
}
