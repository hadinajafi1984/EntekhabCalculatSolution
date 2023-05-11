using BackendApis.Domain;
using BackendApis.Domain.Contracts.Repositories;
using BackendApis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApis.Repositories
{
    public class EfRepository : IRepository
    {
        private readonly AppLicationContext _dbContext;
        private readonly ILogger<EfRepository> _logger;

        public Guid ApiKey { get; set; }
        private readonly IConfiguration _config;

        public EfRepository(AppLicationContext dbContext, ILogger<EfRepository> logger, IConfiguration config)
        {
            _dbContext = dbContext;
            _logger = logger;
            _config = config;
        }
        public async Task<T> GetById<T>(long id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetById<T>(long id, string include) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();
            foreach (string inc in include.Split(','))
            {
                query = query.Include(inc);
            }
            return await query
                .SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<List<T>> List<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (spec != null)
            {
                query = query.Where(spec.Criteria);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> List<T>(int skip, int take, ISpecification<T> spec = null) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (spec != null)
            {
                query = query.Where(spec.Criteria);
            }

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<long> RecordesCount<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (spec != null)
            {
                query = query.Where(spec.Criteria);
            }

            return await query.CountAsync();
        }

        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public async Task Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _dbContext.UpdateRange(entities);
        }

        public async Task DeleteRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            foreach (var item in entities)
            {
                _dbContext.Set<T>().Remove(item);
            }

        }

        public async Task SaveChange()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            foreach (var item in entities)
            {
                _dbContext.Set<T>().Add(item);
            }

        }


    }
}
