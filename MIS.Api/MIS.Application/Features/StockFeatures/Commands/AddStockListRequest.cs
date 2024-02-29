using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Commands
{
    public class AddStockListRequest : IRequest<MessageResponse>
    {
        public AddStockListRequest(List<PostStocksListRequest> postStocksListRequest)
        {
            PostStocksListRequest = postStocksListRequest;
        }

        public List<PostStocksListRequest> PostStocksListRequest { get; set; }
    }
}
