using AutoMapper;
using MIS.Application.Features.BureauFeatures.Commands;
using MIS.Application.Features.ConsumptionTypeFeatures.Commands;
using MIS.Application.Features.DivisionFeatures.Commands;
using MIS.Application.Features.EmployeeTypeFeatures.Commands;
using MIS.Application.Features.EquipmentFeatures.Commands;
using MIS.Application.Features.EquipmentTypeFeatures.Commands;
using MIS.Application.Features.ServiceFeatures.Commands;
using MIS.Application.Features.SupplierFeatures.Commands;
using MIS.Application.Features.SupplierTypeFeatures.Commands;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostBureauStockInventoryRequest, BureauStockInventory>().ReverseMap();
            CreateMap<BureauRequest, Bureau>().ReverseMap();
            CreateMap<BureauResponse, Bureau>().ReverseMap();
            CreateMap<DivisionRequest, Division>().ReverseMap();
            CreateMap<ConsumptionTypeRequest, ConsumptionType>().ReverseMap();
            CreateMap<EquipmentRequest, Equipment>().ReverseMap();
            CreateMap<EquipmentTypeRequest, EquipmentType>().ReverseMap();
            CreateMap<ServiceRequest, Service>().ReverseMap();
            CreateMap<SupplierRequest, Supplier>().ReverseMap();
            CreateMap<EmployeeTypeRequest, EmployeeType>().ReverseMap();
            CreateMap<SupplierTypeRequest, SupplierType>().ReverseMap();
        }
    }
}
