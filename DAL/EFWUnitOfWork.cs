using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class EFWUnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Review> reviewRepository;

        public EFWUnitOfWork(AppDBContext context)
        {
            this.context = context;
            gameRepository = new EntityRepository<Game>(this.context);
            reviewRepository = new EntityRepository<Review>(this.context);
        }

        public IRepository<Game> GameRepository => gameRepository;
        public IRepository<Review> ReviewRepository => reviewRepository;

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
