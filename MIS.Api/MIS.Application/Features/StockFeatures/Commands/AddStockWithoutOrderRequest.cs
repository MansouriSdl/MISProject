using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Commands
{
    public class AddStockWithoutOrderRequest : IRequest<MessageResponse>
    {
        public AddStockWithoutOrderRequest(string designation, int quantity, Guid equipmentId, bool isMultiple, DateTime? expirationDate)
        {
            Designation = designation;
            Quantity = quantity;
            EquipmentId = equipmentId;
            IsMultiple = isMultiple;
            ExpirationDate = expirationDate;
        }

        public string Designation { get; set; }
        public int Quantity { get; set; }
        public Guid EquipmentId { get; set; }
        public bool IsMultiple { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
