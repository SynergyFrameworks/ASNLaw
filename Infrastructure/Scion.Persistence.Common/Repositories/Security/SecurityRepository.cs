using Microsoft.EntityFrameworkCore;
using Scion.Infrastructure.Domain;
using Scion.Infrastructure.Security.Model;
using Scion.Data;

using System.Linq;
using Scion.Data.Common;

namespace Scion.Infrastructure.Security.Repositories
{
    public class SecurityRepository : DbContextRepositoryBase<SecurityDbContext>, ISecurityRepository
    {
        private bool disposedValue;

        public SecurityRepository(SecurityDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual IQueryable<UserApiKeyEntity> UserApiKeys { get { return DbContext.Set<UserApiKeyEntity>(); } }

        public IUnitOfWork UnitOfWork => throw new System.NotImplementedException();

        public void Add<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Attach<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Remove<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SecurityRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
