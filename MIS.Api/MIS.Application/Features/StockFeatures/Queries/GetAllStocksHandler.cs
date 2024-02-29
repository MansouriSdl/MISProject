using MediatR;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Queries
{
    public class GetAllStocksHandler : IRequestHandler<GetAllStocks, IEnumerable<Stock>>
    {
        private readonly IStockRepository _stockRepository;

        public GetAllStocksHandler(IStockRepository equipmentRepository)
        {
            _stockRepository = equipmentRepository;
        }

        public Task<IEnumerable<Stock>> Handle(GetAllStocks request, CancellationToken cancellationToken)
        {
            return _stockRepository.GetAllStocks();
        }
    }
}
