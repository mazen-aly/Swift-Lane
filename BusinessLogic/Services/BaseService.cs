using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Enums;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class BaseService<TDomainModel, TDTO> : IBaseService<TDomainModel, TDTO> where TDomainModel : BaseEntity where TDTO : BaseDto
    {
        private readonly IRepository<TDomainModel> _repo;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public BaseService(IRepository<TDomainModel> repo, IMapper mapper, ISecurityService securityService)
        {
            _repo = repo;
            _mapper = mapper;
            _securityService = securityService;
        }


        public async Task<List<TDTO>?> GetAllAsync()
        {
            var domainModelsList = await _repo.GetAllAsync();

            return _mapper.Map<List<TDomainModel>, List<TDTO>?>(domainModelsList);
        }


        public async Task<TDTO?> GetByIdAsync(Guid id)
        {
            var domainModel = await _repo.GetByIdAsync(id);

            return _mapper.Map<TDTO>(domainModel);
        }


        public async Task<bool> AddNewAsync(TDTO domainModelDTO)
        {
            var domainModel = _mapper.Map<TDomainModel>(domainModelDTO);
            domainModel.CreatedDate = DateTime.Now;
            domainModel.CreatedBy = _securityService.GetLoggedInUserId();

            return await _repo.AddNewAsync(domainModel);
        }


        public async Task<bool> UpdateAsync(TDTO domainModelDTO)
        {
            if (!await _repo.CheckExistenceAsync(domainModelDTO.Id))
            {
                return false;
            }

            var domainModel = await _repo.GetByIdAsync(domainModelDTO.Id);

            _mapper.Map(domainModelDTO, domainModel);

            domainModel.UpdatedDate = DateTime.Now;
            domainModel.UpdatedBy = _securityService.GetLoggedInUserId();

            return await _repo.UpdateAsync(domainModel);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var domainModel = await _repo.GetByIdAsync(id);

            if (domainModel is null)
            {
                return false;
            }

            // Soft deletes the entity
            domainModel.CurrentState = Convert.ToInt32(DatabaseRecordState.Deleted);
            domainModel.UpdatedDate = DateTime.Now;
            domainModel.UpdatedBy = _securityService.GetLoggedInUserId();

            return await _repo.UpdateAsync(domainModel);
        }
    }
}
