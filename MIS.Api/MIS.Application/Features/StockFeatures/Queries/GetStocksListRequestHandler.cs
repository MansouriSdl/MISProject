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
    public class GetStocksListRequestHandler : IRequestHandler<GetStocksListRequest, List<StockResponse>>
    {
        private readonly IStockRepository _stockRepository;

        public GetStocksListRequestHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<List<StockResponse>> Handle(GetStocksListRequest request, CancellationToken cancellationToken)
        {
            return _stockRepository.GetStockList(request.FilterRequest);
        }
    }
}
