using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentFeatures.Commands
{
    public class EquipmentRequest : IRequest<MessageResponse>
    {
        public EquipmentRequest(string name, string description, string serialNumber, Guid equipmentTypeId, Guid consumptionTypeId, int stockLimit, int? expirationDateLimit)
        {
            Name = name;
            Description = description;
            SerialNumber = serialNumber;
            EquipmentTypeId = equipmentTypeId;
            ConsumptionTypeId = consumptionTypeId;
            StockLimit = stockLimit;
            ExpirationDateLimit = expirationDateLimit;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public Guid EquipmentTypeId { get; set; }
        public Guid ConsumptionTypeId { get; set; }
        public int StockLimit { get; set; }
        public int? ExpirationDateLimit { get; set; }
    }
}
