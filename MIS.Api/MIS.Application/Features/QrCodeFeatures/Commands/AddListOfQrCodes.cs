using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.QrCodeFeatures.Commands
{
    public class AddListOfQrCodes : IRequest<List<QrCodeResponse>>
    {
        public Guid BureauId { get; set; }
        public int QrCodesNumber { get; set; }
    }
}
