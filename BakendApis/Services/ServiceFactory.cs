using BackendApis.Repositories;

namespace BackendApis.Services
{
    public interface IServiceFactory
    {
        public AppServices AppServices { get; }
        Task<int> SaveAsync();
    }

    public class ServiceFactory : IDisposable, IServiceFactory
    {
        public Guid ApiKey { get; set; }

        private readonly IRepositoryFactory _factory;
        private readonly IConfiguration _configuration;
        public ServiceFactory(IRepositoryFactory repositoryFactory, IConfiguration configuration)
        {
            _factory = repositoryFactory;
            _configuration = configuration;
        }

        private AppServices _appServices;
        public AppServices AppServices
        {
            get
            {
                this._appServices ??= new AppServices(_factory, _configuration);
                return _appServices;
            }
        }



        #region SaveChange
        public async Task<int> SaveAsync()
        {
            return await _factory.SaveAsync();
        }
        #endregion

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ((IDisposable)_factory).Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
