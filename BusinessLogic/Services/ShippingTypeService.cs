using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ShippingTypeService : BaseService<ShippingType, ShippingTypeDto>, IShippingTypeService
    {
        private readonly IRepository<ShippingType> _repo;
        private readonly IMapper _mapper;

        public ShippingTypeService(IRepository<ShippingType> repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


    }
}
