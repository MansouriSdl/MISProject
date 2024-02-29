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
    public class GetAlertsRequestHandler : IRequestHandler<GetAlertsRequest, AlertResponse>
    {
        private readonly IStockRepository _stockRepository;

        public GetAlertsRequestHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<AlertResponse> Handle(GetAlertsRequest request, CancellationToken cancellationToken)
        {
            return _stockRepository.GetAlerts();
        }
    }
}
