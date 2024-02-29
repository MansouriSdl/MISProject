using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Attachment : AuditEntity
    {
        public byte[] File { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }
        public string Extention { get; set; }
        public string Description { get; set; }
        public Order Order { get; set; }
    }
}
