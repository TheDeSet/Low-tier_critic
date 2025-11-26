using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer
{
    internal class DapperUnitOfWork : IUnitOfWork
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Projects\\Homework\\C#\\Lab 3.1\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security = True";
        private readonly SqlConnection connection;
        private readonly SqlTransaction? transaction;
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Review> reviewRepository;

        public DapperUnitOfWork()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
            // Передаём транзакцию в репозитории
            gameRepository = new DapperRepository<Game>(connection, transaction);
            reviewRepository = new DapperRepository<Review>(connection, transaction);
        }

        public IRepository<Game> GameRepository => gameRepository;
        public IRepository<Review> ReviewRepository => reviewRepository;

        public int SaveChanges()
        {
            if (transaction == null) throw new InvalidOperationException("Транзакция не начата.");
            transaction.Commit();
            connection.Close();
            return 1; // условно
        }

        public async Task<int> SaveChangesAsync()
        {
            if (transaction == null) throw new InvalidOperationException("Транзакция не начата.");
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return 1; // условно
        }

        public void Dispose()
        {
            transaction?.Dispose();
            connection?.Dispose();
        }
    }
}
