using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scion.Infrastructure.Domain;
using Scion.Infrastructure.Exceptions;

namespace Scion.Data.Common
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public DbContext DbContext { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DbContextUnitOfWork(DbContext context)
        {
            DbContext = context;
        }

        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
