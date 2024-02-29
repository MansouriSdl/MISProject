using MIS.Application.Features.StockFeatures.Commands;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetAllStocks();
        Task<List<StockResponse>> GetStockList(FilterRequest request);
        Task<MessageResponse> AddStock(AddStockWithoutOrderRequest request);
        Task<MessageResponse> AddStockList(List<PostStocksListRequest> request);
        Task<IEnumerable<NameResponse>> GetStocksByEquipmentId(Guid equipmentId);

        //Alert

        Task<AlertResponse> GetAlerts();

        //charts
        Task<List<NameValueResponse>> TopFiveConsummableEquipments(FilterRequest request);

    }
}
