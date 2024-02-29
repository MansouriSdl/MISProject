using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Queries
{
    public class TopFiveConsummableEquipmentsRequestHandler : IRequestHandler<TopFiveConsummableEquipmentsRequest, List<NameValueResponse>>
    {
        private readonly IStockRepository _stockRepository;

        public TopFiveConsummableEquipmentsRequestHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<List<NameValueResponse>> Handle(TopFiveConsummableEquipmentsRequest request, CancellationToken cancellationToken)
        {
            return _stockRepository.TopFiveConsummableEquipments(request.FilterRequest);
        }
    }
}
