using BackendApis.Domain.Entities;

namespace BackendApis.Domain.Contracts.Repositories
{
    public interface IRepository
    {

        Task<T> GetById<T>(long id) where T : BaseEntity;
        Task<T> GetById<T>(long id, string include) where T : BaseEntity;
        Task<List<T>> List<T>(ISpecification<T> spec = null) where T : BaseEntity;
        Task<List<T>> List<T>(int skip, int take, ISpecification<T> spec = null) where T : BaseEntity;
        Task<long> RecordesCount<T>(ISpecification<T> spec = null) where T : BaseEntity;
        Task<T> Add<T>(T entity) where T : BaseEntity;

        Task AddRange<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task Update<T>(T entity) where T : BaseEntity;
        Task UpdateRange<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task Delete<T>(T entity) where T : BaseEntity;
        Task DeleteRange<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task SaveChange();



    }
}
