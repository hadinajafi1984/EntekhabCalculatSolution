using BackendApis.Domain.Contracts.Repositories;
using BackendApis.Domain.Contracts.Services;
using BackendApis.Domain.Entities;
using BackendApis.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BackendApis.Services
{
    public class AppServices : IService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IConfiguration _configuration;
        public AppServices(IRepositoryFactory repositoryFactory, IConfiguration configuration)
        {
            _repository = repositoryFactory;
            _configuration = configuration;
        }
        public async Task<T> AddNew<T>(T item) where T : BaseEntity
        {
            return await _repository.Repository.Add<T>(item);
        }

        public async Task AddNewRange<T>(IEnumerable<T> list) where T : BaseEntity
        {
            await _repository.Repository.AddRange<T>(list);
        }

        public async Task<IEnumerable<T>> GetALL<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await _repository.Repository.List<T>(spec);
        }

        public async Task<IEnumerable<T>> GetALL<T>(int skip, int take, ISpecification<T> spec = null) where T : BaseEntity
        {
            return await _repository.Repository.List<T>(skip, take, spec);
        }

        public async Task<IEnumerable<T>> GetRangeWithDapper<T>(DateTime sDate, DateTime eDate) where T : BaseEntity
        {
            var connectionString = _configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            if (typeof(T) == typeof(PersonnelData))
            {
                using (var con = new SqlConnection(connectionString))
                {
                    
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("startDate", sDate);
                    parameters.Add("endDate", eDate);

                    var data = con.Query<PersonnelData>("GetPersonelWithDate", parameters, commandType: CommandType.StoredProcedure);
                    return data as IEnumerable<T>;
                }
            }

            return null;
        }

        public async Task<T> GetById<T>(long id) where T : BaseEntity
        {
            return await _repository.Repository.GetById<T>(id);
        }
        public async Task<T> GetById<T>(long id, string include) where T : BaseEntity
        {
            return await _repository.Repository.GetById<T>(id, include);
        }

        public async Task<T> GetByIdWitDapper<T>(string firstname, string lastname) where T : BaseEntity
        {
            var connectionString = _configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            if (typeof(T)==typeof(PersonnelData))
            {
                using (var con = new SqlConnection(connectionString))
                {
                   
                      
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("firstname", firstname);
                    parameters.Add("lastname", lastname);

                    var date = con.QuerySingleOrDefault<PersonnelData>("GetPesonelWithName", parameters, commandType: CommandType.StoredProcedure);
                    return date as T;
                }
            }

            return null;
        }

        public async Task<long> RecordesCount<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await _repository.Repository.RecordesCount<T>(spec);
        }

        public async Task Remove<T>(T item) where T : BaseEntity
        {
            await _repository.Repository.Delete<T>(item);
        }

        public async Task RemoveRange<T>(IEnumerable<T> list) where T : BaseEntity
        {
            await _repository.Repository.DeleteRange<T>(list);
        }

        public async Task Update<T>(T item) where T : BaseEntity
        {
            await _repository.Repository.Update<T>(item);
        }

        public async Task UpdateRange<T>(IEnumerable<T> list) where T : BaseEntity
        {
            await _repository.Repository.UpdateRange<T>(list);
        }
    }
}
