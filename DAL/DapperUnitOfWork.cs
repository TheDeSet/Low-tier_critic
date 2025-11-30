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
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\CloneGIT\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security=True";
        //private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Projects\\Homework\\C#\\Lab 3.1\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security = True";
        private  SqlConnection connection;
        private  SqlTransaction? transaction;

        private bool started = false;

        public IRepository<Game> GameRepository { get; private set; }
        public IRepository<Review> ReviewRepository { get; private set; }

        public void Begin()
        {
            if (started) return;

            connection = new SqlConnection(connectionString);
            connection.Open();

            transaction = connection.BeginTransaction();

            GameRepository = new DapperRepository<Game>(connection, transaction);
            ReviewRepository = new DapperRepository<Review>(connection, transaction);

            started = true;
        }

        public int SaveChanges()
        {
            try
            {
                if (!started || transaction == null) throw new InvalidOperationException("Транзакция не начата.");
                transaction.Commit();
                return 0;
            }
            catch
            {
                transaction?.Rollback();
                throw new InvalidOperationException("Транзакция не удалась. Возможно имеются ошибки в бд.");
            }
            finally
            {
                connection.Close();
                Dispose();
                started = false;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                if (transaction == null) throw new InvalidOperationException("Транзакция не начата.");
                await transaction.CommitAsync();
                await connection.CloseAsync();
                return 0;
            }
            catch
            {
                await transaction?.RollbackAsync();
                throw new InvalidOperationException("Транзакция не удалась. Возможно имеются ошибки в бд.");
            }
            finally
            {
                await connection.CloseAsync();
                Dispose();
                started = false;
            }
        }

        public void Dispose()
        {
            transaction?.Dispose();
            connection?.Dispose();
            transaction = null;
            started = false;
        }
    }
}
