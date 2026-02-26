using DataAccess.Enums;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class Repository<TDomainModel> : IRepository<TDomainModel> where TDomainModel : BaseEntity
    {
        private readonly SwiftLaneDbContext _context;
        private readonly DbSet<TDomainModel> _table;
        private readonly ILogger<Repository<TDomainModel>> _logger;

        public Repository(SwiftLaneDbContext context, ILogger<Repository<TDomainModel>> logger)
        {
            _context = context;
            _table = _context.Set<TDomainModel>();
            _logger = logger;
        }

        public async Task<List<TDomainModel>> GetAllAsync()
        {
            try
            {
                return await _table.Where(e => e.CurrentState == Convert.ToInt32(DatabaseRecordState.Existant)).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all records from database.");
                throw;
            }
        }

        public async Task<TDomainModel?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _table.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve record with id: {id} from database.");
                throw;
            }
        }
        
        public async Task<bool> AddNewAsync(TDomainModel newDomainModel)
        {
            try
            {
                await _table.AddAsync(newDomainModel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add new record to database.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TDomainModel updatedDomainModel)
        {
            try
            {
                _table.Update(updatedDomainModel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record in database.");
                throw;
            }
        }

        public async Task<bool> CheckExistenceAsync(Guid id)
        {
            try
            {
                return await _table.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check existence of the required record in database.");
                throw;
            }
        }
    }
}
