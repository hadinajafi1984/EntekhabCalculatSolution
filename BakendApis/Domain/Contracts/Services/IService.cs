using BackendApis.Domain.Contracts.Repositories;
using BackendApis.Domain.Entities;

namespace BackendApis.Domain.Contracts.Services
{
    public interface IService
    {
        Task<T> AddNew<T>(T item) where T : BaseEntity;
        Task AddNewRange<T>(IEnumerable<T> list) where T : BaseEntity;
        Task<IEnumerable<T>> GetALL<T>(ISpecification<T> spec = null) where T : BaseEntity;
        Task<IEnumerable<T>> GetALL<T>(int skip, int take, ISpecification<T> spec = null) where T : BaseEntity;
        Task<long> RecordesCount<T>(ISpecification<T> spec = null) where T : BaseEntity;
        Task<T> GetById<T>(long id) where T : BaseEntity;
        Task<T> GetById<T>(long id, string include) where T : BaseEntity;
        Task Remove<T>(T item) where T : BaseEntity;
        Task RemoveRange<T>(IEnumerable<T> list) where T : BaseEntity;
        Task Update<T>(T item) where T : BaseEntity;
        Task UpdateRange<T>(IEnumerable<T> list) where T : BaseEntity;

        Task<IEnumerable<T>> GetRangeWithDapper<T>(DateTime sDate, DateTime eDate) where T : BaseEntity;

        Task<T> GetByIdWitDapper<T>(string firstname,string lastname) where T : BaseEntity;
    }
}
