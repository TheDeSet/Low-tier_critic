using Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace DataAccessLayer
{
    public class EFWUnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;
        private IDbContextTransaction? transaction;
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Review> reviewRepository;
        private bool started = false;
        public EFWUnitOfWork(AppDBContext context)
        {
            this.context = context;
            gameRepository = new EntityRepository<Game>(this.context);
            reviewRepository = new EntityRepository<Review>(this.context);
        }

        public IRepository<Game> GameRepository => gameRepository;
        public IRepository<Review> ReviewRepository => reviewRepository;
        public void Begin()
        {
            if (started) return;

            transaction = context.Database.BeginTransaction();
            started = true;
        }
        public int SaveChanges()
        {
            try
            {
                if (!started || transaction == null) throw new InvalidOperationException("Транзакция не начата.");
                int result = context.SaveChanges(); 
                transaction?.Commit();              
                return result;
            }
            catch
            {
                transaction?.Rollback();
                throw new InvalidOperationException("Транзакция не удалась. Возможно имеются ошибки в бд.");
            }
            finally
            {
                Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                if (!started || transaction == null) throw new InvalidOperationException("Транзакция не начата.");
                int result = await context.SaveChangesAsync();
                await transaction!.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Транзакция не удалась. Возможно имеются ошибки в бд.");
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            transaction?.Dispose();
            transaction = null;
            started = false;
        }
    }
}
