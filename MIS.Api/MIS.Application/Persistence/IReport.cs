using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IReport
    {
        Task<IEnumerable<MonthlyOrderReportModel>> GenerateMonthlyOrderReportAsync(int year);
        Task<IEnumerable<OrderStatusReportModel>> GenerateOrderStatusReportAsync(PredicateDate predicateDate);
        Task<IEnumerable<EquipmentUsageReportModel>> GenerateEquipmentUsageReportAsync(PredicateDate predicateDate);
        Task<IEnumerable<InventoryLevelReportModel>> GenerateCurrentInventoryLevelsReport();
        Task<IEnumerable<MostConsumedEquipmentModel>> GenerateMostConsumedEquipmentReport();
    }
}
