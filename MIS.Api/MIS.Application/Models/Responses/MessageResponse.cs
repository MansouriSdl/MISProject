using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class MessageResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static explicit operator Task<object>(MessageResponse v)
        {
            throw new NotImplementedException();
        }
    }
}
