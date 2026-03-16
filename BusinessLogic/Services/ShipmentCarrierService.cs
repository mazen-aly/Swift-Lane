using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class ShipmentCarrierService : BaseService<ShipmentCarrier, ShipmentCarrierDto>, IShipmentCarrierService
    {
        private readonly IRepository<ShipmentCarrier> _repo;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;



        public ShipmentCarrierService(IRepository<ShipmentCarrier> repo, IMapper mapper, ISecurityService securityService)
            : base(repo, mapper, securityService)
        {
            _repo = repo;
            _mapper = mapper;
            _securityService = securityService;
        }
    }
}
