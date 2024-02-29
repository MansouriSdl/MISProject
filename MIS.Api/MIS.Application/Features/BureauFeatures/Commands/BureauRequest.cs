using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Commands
{
    public class BureauRequest : IRequest<MessageResponse>
    {
        public BureauRequest(string name, string abbreviation, Guid serviceId)
        {
            Name = name;
            Abbreviation = abbreviation;
            ServiceId = serviceId;
        }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Guid ServiceId { get; set; }
    }
}
