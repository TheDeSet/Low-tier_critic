using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Game> GameRepository { get; }
        IRepository<Review> ReviewRepository { get; }
        void Begin();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
