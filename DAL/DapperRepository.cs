using Dapper;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer
{  
    public class GameDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public int YearOfRelease { get; set; }
        public string Platforms { get; set; }
        public float? Rating { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; }
        public string? Screenshots { get; set; }
        public string? Reviews { get; set; }
    }

    public class DapperRepository <T>(IDbConnection connection, IDbTransaction? transaction) : IRepository<T> where T : IDomainObject
    {
        private readonly IDbConnection connection = connection;
        private readonly IDbTransaction? transaction = transaction;

        //readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\CloneGIT\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security=True";

        /// <summary>
        /// Сериализует список EnumPlatforms в строку, разделённую запятыми.
        /// </summary>
        /// <param name="platforms">Список платформ для сериализации.</param>
        /// <returns>Строка, представляющая список платформ.</returns>
        private string SerializePlatforms(List<EnumPlatforms> platforms)
        {
            return JsonSerializer.Serialize(platforms, new JsonSerializerOptions { WriteIndented = false });
        }

        /// <summary>
        /// Сериализует список строк путей к скриншотам в JSON-строку.
        /// </summary>
        /// <param name="images">Список путей скриншотов для сериализации.</param>
        /// <returns>JSON-строка, представляющая список путей скриншотов.</returns>
        private string SerializeScreenshots(List<string> images)
        {
            return JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = false });
        }

        /// <summary>
        /// Десериализует строку, представляющую список значений Enum, в список T.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="platforms">Строка, содержащая значения перечисления.</param>
        /// <returns>Список значений перечисления типа T.</returns>
        private List<EnumPlatforms> DeserializePlatforms(string platforms)
        {
            var platformInts = JsonSerializer.Deserialize<List<int>>(platforms);
            if (platformInts == null)
                return new List<EnumPlatforms>();

            return platformInts
                .Where(i => Enum.IsDefined(typeof(EnumPlatforms), i))
                .Select(i => (EnumPlatforms)i)
                .ToList();
        }

        /// <summary>
        /// Десериализует JSON-строку, представляющую список путей скриншотов, в список строк.
        /// </summary>
        /// <param name="screenshotsString">JSON-строка с путями скриншотов.</param>
        /// <returns>Список строк путей скриншотов.</returns>
        private List<string> DeserializeScreenshots(string screenshotsString)
        {
            if (string.IsNullOrEmpty(screenshotsString))
            {
                return new List<string>();
            }
            var list = JsonSerializer.Deserialize<List<string>>(screenshotsString);
            return list ?? new List<string>();
        }

        public void Add(T entity)
        {   
            if (typeof(T) == typeof(Entities.Game))
            {
                Game game = entity as Game;
                string sqlQuery = @"INSERT INTO Games (Name, Developer, YearOfRelease, Platforms, Rating, Description, Icon, Screenshots) " +
                    "VALUES (@Name, @Developer, @YearOfRelease, @Platforms, @Rating, @Description, @Icon, @Screenshots)";
                var parameters = new
                {
                    Name = game.Name,
                    Developer = game.Developer,
                    YearOfRelease = game.YearOfRelease,
                    Platforms = SerializePlatforms(game.Platforms),
                    Rating = game.Rating,
                    Description = game.Description,
                    Icon = game.Icon,
                    Screenshots = SerializeScreenshots(game.Screenshots),
                };
                /*using IDbConnection connection = new SqlConnection(ConnectionString);*/
                connection.Execute(sqlQuery, parameters, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"INSERT INTO Reviews (Username, Rating, ReviewText, GameId) " +
                    "VALUES (@Username, @Rating, @ReviewText, @GameId)";
                /*using IDbConnection connection = new SqlConnection(ConnectionString);*/
                connection.Execute(sqlQuery, review, transaction);
            }
        }

        public void Delete<T>(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                /*string sqlQuery = @"DELETE FROM Games WHERE ID = @ID";
                using IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Execute(sqlQuery, new { ID = id });*/
                
                // Удаляем отзывы, связанные с игрой
                connection.Execute("DELETE FROM Reviews WHERE GameId = @ID", new { ID = id }, transaction);
                // Удаляем саму игру
                connection.Execute("DELETE FROM Games WHERE ID = @ID", new { ID = id }, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                /*string sqlQuery = @"DELETE FROM Reviews WHERE ID = @ID";
                using IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Execute(sqlQuery, new { ID = id });*/

                connection.Execute("DELETE FROM Reviews WHERE ID = @ID", new { ID = id }, transaction);
            }
        }

        public void Update(T entity)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                Game game = entity as Game;
                game.Platforms ??= new List<EnumPlatforms>();
                game.Screenshots ??= new List<string>();
                string sqlQuery = @"UPDATE Games " + 
                    "SET Name = @Name, Developer = @Developer, YearOfRelease = @YearOfRelease, Platforms = @Platforms, Rating = @Rating, Description = @Description, Icon = @Icon, Screenshots = @Screenshots " +
                    "WHERE ID = @ID";
                var parameters = new
                {
                    ID = game.ID,
                    Name = game.Name,
                    Developer = game.Developer,
                    YearOfRelease = game.YearOfRelease,
                    Platforms = SerializePlatforms(game.Platforms),
                    Rating = game.Rating,
                    Description = game.Description,
                    Icon = game.Icon,
                    Screenshots = SerializeScreenshots(game.Screenshots),
                };
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Execute(sqlQuery, parameters, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"UPDATE Reviews " +
                    "SET Username = @Username, Rating = @Rating, ReviewText = @ReviewText, GameId = @GameId" +
                    "WHERE ID = @ID";
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Execute(sqlQuery, review, transaction);
            }
        }

        public T? ReadById(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                string sqlQuery = @"SELECT * FROM Games WHERE ID = @ID";
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                GameDTO gameDTO = connection.QueryFirstOrDefault<GameDTO>(sqlQuery, new { ID = id }, transaction);
                Game game = new Game();
                game.ID = gameDTO.ID;
                game.Name = gameDTO.Name;
                game.Developer = gameDTO.Developer;
                game.YearOfRelease = gameDTO.YearOfRelease;
                game.Platforms = DeserializePlatforms(gameDTO.Platforms);
                game.Rating = gameDTO.Rating;
                game.Description = gameDTO.Description;
                game.Icon = gameDTO.Icon;
                game.Screenshots = DeserializeScreenshots(gameDTO.Screenshots);

                string reviewsQuery = @"SELECT * FROM Reviews WHERE GameId = @GameId";
                List<Review> reviews = connection.Query<Review>(reviewsQuery, new { GameId = gameDTO.ID }, transaction).AsList();
                game.Reviews = reviews ?? new List<Review>();

                return (T)(object)game;
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = @"SELECT * FROM Reviews WHERE ID = @ID";
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                Review review = connection.QueryFirstOrDefault<Review>(sqlQuery, new { ID = id }, transaction);
                return (T)(object)review;
            }
            else
            {
                return default;
            }
        }

        public List<T> ReadAll()
        {
            List<T> outputList = new List<T>();
            if (typeof(T) == typeof(Entities.Game))
            {
                string sqlQuery = "SELECT * FROM Games ORDER BY ID";
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                List<GameDTO> gamesDTO = connection.Query<GameDTO>(sqlQuery, transaction:transaction).AsList();
                foreach (GameDTO gameDTO in gamesDTO)
                {
                    Game game = new Game();
                    game.ID = gameDTO.ID;
                    game.Name = gameDTO.Name;
                    game.Developer = gameDTO.Developer;
                    game.YearOfRelease = gameDTO.YearOfRelease;
                    game.Platforms = DeserializePlatforms(gameDTO.Platforms);
                    game.Rating = gameDTO.Rating;
                    game.Description = gameDTO.Description;
                    game.Icon = gameDTO.Icon;
                    game.Screenshots = DeserializeScreenshots(gameDTO.Screenshots);
                    
                    string reviewsQuery = @"SELECT * FROM Reviews WHERE GameId = @GameId";
                    List<Review> reviews = connection.Query<Review>(reviewsQuery, new { GameId = gameDTO.ID }, transaction).AsList();
                    game.Reviews = reviews ?? new List<Review>();
                    outputList.Add((T)(object)game);
                }
            }
            else if(typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = "SELECT * FROM Reviews ORDER BY ID";
                //using IDbConnection connection = new SqlConnection(ConnectionString);
                List<Review> reviews = connection.Query<Review>(sqlQuery, transaction: transaction).AsList();
                foreach (Review review in reviews)
                {
                    outputList.Add((T)(object)review);
                }
            }
            return outputList;
        }
    }
}
