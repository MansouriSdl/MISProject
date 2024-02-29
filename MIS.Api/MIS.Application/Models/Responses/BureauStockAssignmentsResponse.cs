using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class BureauStockAssignmentsResponse
    {
        public Guid Id { get; set; }
        public Guid BureauId { get; set; }
        public string Bureau { get; set; }
        public Guid StockId { get; set; }
        public string Stock { get; set; }
        public string Equipment { get; set; }
        public string AssignmentDate { get; set; }
        public int Quantity { get; set; }
        public string User { get; set; }

    }
}
