using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostQrCodesListRequest
    {
        public Guid BureauId { get; set; }
        public int QrCodesNumber { get; set; }                                                                                                                                                                                                                                                                                    
    }
}
