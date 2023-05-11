using BackendApis.Domain;
using BackendApis.Domain.Contracts.Repositories;

namespace BackendApis.Repositories
{
    public interface IRepositoryFactory
    {
        public IRepository Repository { get; }
        Task<int> SaveAsync();
    }
    public class RepositoryFactory : IDisposable, IRepositoryFactory
    {
        private readonly AppLicationContext _context;

        public RepositoryFactory(AppLicationContext context, IRepository rep)
        {
            _context = context;
            Repository = rep;
        }



        public IRepository Repository { get; }


        #region SaveChange

        public async Task<int> SaveAsync()
        {

            int result = await _context.SaveChangesAsync();
            return result;

        }

        #endregion

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
