using AutoMapper;
using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shipment, ShipmentDto>().ReverseMap();
            CreateMap<ShipmentCarrier, ShipmentCarrierDto>().ReverseMap();
            CreateMap<UserSender, UserSenderDto>().ReverseMap();
            CreateMap<UserReceiver, UserReceiverDto>().ReverseMap();
            CreateMap<ShippingType, ShippingTypeDto>().ReverseMap();

        }
    }
}
