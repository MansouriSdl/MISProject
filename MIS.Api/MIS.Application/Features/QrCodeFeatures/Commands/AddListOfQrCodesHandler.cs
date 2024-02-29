using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.QrCodeFeatures.Commands
{
    public class AddListOfQrCodesHandler : IRequestHandler<AddListOfQrCodes, List<QrCodeResponse>>
    {
        private readonly IQrCodeRepository _qrCodeRepository;

        public AddListOfQrCodesHandler(IQrCodeRepository qrCodeRepository)
        {
            _qrCodeRepository = qrCodeRepository;
        }

        public async Task<List<QrCodeResponse>> Handle(AddListOfQrCodes request, CancellationToken cancellationToken)
        {
            PostQrCodesListRequest postQrCodesListRequest = new ()
            {
                BureauId = request.BureauId,
                QrCodesNumber = request.QrCodesNumber
            };
            return await _qrCodeRepository.AddListOfQrCodes(postQrCodesListRequest);
        }
    }
}
