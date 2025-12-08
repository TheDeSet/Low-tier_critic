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
        /// <summary>
        /// Начинает новую транзакцию базы данных.
        /// </summary>
        void TransactionBegin();
        /// <summary>
        /// Сохраняет все изменения и фиксирует транзакцию.
        /// </summary>
        int SaveChanges();
        /// <summary>
        /// Асинхронно фиксирует текущую транзакцию, сохраняя все изменения в базе данных.
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
