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
    public interface IQrCodeRepository : IRepository<QrCode>
    {
        Task<List<QrCodeResponse>> AddListOfQrCodes(PostQrCodesListRequest request);

    }
}
