using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Commands
{
    public class AddStockWithoutOrderRequestHandler : IRequestHandler<AddStockWithoutOrderRequest, MessageResponse>
    {
        private readonly IStockRepository _stockRepository;

        public AddStockWithoutOrderRequestHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<MessageResponse> Handle(AddStockWithoutOrderRequest request, CancellationToken cancellationToken)
        {
            return _stockRepository.AddStock(request);
        }
    }
}
