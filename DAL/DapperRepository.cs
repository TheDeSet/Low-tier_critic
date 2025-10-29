using Dapper;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer
{
    public class DapperRepository <T> : IRepository<T> where T : IDomainObject
    {
        readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Projects\\Homework\\C#\\Lab 3.1\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security=True";

        public void Add(T entity)
        {   
            if (typeof(T) == typeof(Entities.Game))
            {
                Game game = entity as Game;
                game.Platforms ??= new List<EnumPlatforms>();
                game.Screenshots ??= new List<string>();
                game.Reviews ??= new List<Review>();
                //Тут будет сериализация списков
                string sqlQuery = @"INSERT INTO Games (ID, Name, Developer, YearOfRelease, Platforms, Rating, Description, Icon, Screenshots) " +
                    "VALUES (@ID, @Name, @Developer, @YearOfRelease, @Platforms, @Rating, @Description, @Icon, @Screenshots)";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, game);
            }  
        }

        public void Delete(int id)
        {

        }

        public void Update(T entity)
        {

        }

        public T? ReadById(int id)
        {
            return default;
        }

        public List<T> ReadAll()
        {
            return null;
        }
    }
}
