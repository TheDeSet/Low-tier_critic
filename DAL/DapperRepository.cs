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

    public class DapperRepository <T>(IDbConnection connection, IDbTransaction? transaction) : IRepository<T> where T : IDomainObject
    {
        private readonly IDbConnection connection = connection;
        private readonly IDbTransaction? transaction = transaction;
        private readonly IGameDataSerializer gameSerializer = new GameDataSerializer();

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
                    Platforms = gameSerializer.SerializeList(game.Platforms),
                    Rating = game.Rating,
                    Description = game.Description,
                    Icon = game.Icon,
                    Screenshots = gameSerializer.SerializeList(game.Screenshots),
                };
                connection.Execute(sqlQuery, parameters, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"INSERT INTO Reviews (Username, Rating, ReviewText, GameId) " +
                    "VALUES (@Username, @Rating, @ReviewText, @GameId)";
                connection.Execute(sqlQuery, review, transaction);
            }
        }

        public void Delete<T>(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                // Удаляем отзывы, связанные с игрой
                connection.Execute("DELETE FROM Reviews WHERE GameId = @ID", new { ID = id }, transaction);
                // Удаляем саму игру
                connection.Execute("DELETE FROM Games WHERE ID = @ID", new { ID = id }, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
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
                    Platforms = gameSerializer.SerializeList(game.Platforms),
                    Rating = game.Rating,
                    Description = game.Description,
                    Icon = game.Icon,
                    Screenshots = gameSerializer.SerializeList(game.Screenshots),
                };
                connection.Execute(sqlQuery, parameters, transaction);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"UPDATE Reviews " +
                    "SET Username = @Username, Rating = @Rating, ReviewText = @ReviewText, GameId = @GameId" +
                    "WHERE ID = @ID";
                connection.Execute(sqlQuery, review, transaction);
            }
        }

        public T? ReadById(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                string sqlQuery = @"SELECT * FROM Games WHERE ID = @ID";
                GameDTO gameDTO = connection.QueryFirstOrDefault<GameDTO>(sqlQuery, new { ID = id }, transaction);
                Game game = new Game();
                game.ID = gameDTO.ID;
                game.Name = gameDTO.Name;
                game.Developer = gameDTO.Developer;
                game.YearOfRelease = gameDTO.YearOfRelease;
                game.Platforms = gameSerializer.DeserializeToEnumOfPlatforms(gameDTO.Platforms);
                game.Rating = gameDTO.Rating;
                game.Description = gameDTO.Description;
                game.Icon = gameDTO.Icon;
                game.Screenshots = gameSerializer.DeserializeToListOfStrings(gameDTO.Screenshots);

                string reviewsQuery = @"SELECT * FROM Reviews WHERE GameId = @GameId";
                List<Review> reviews = connection.Query<Review>(reviewsQuery, new { GameId = gameDTO.ID }, transaction).AsList();
                game.Reviews = reviews ?? new List<Review>();

                return (T)(object)game;
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = @"SELECT * FROM Reviews WHERE ID = @ID";
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
                List<GameDTO> gamesDTO = connection.Query<GameDTO>(sqlQuery, transaction:transaction).AsList();
                foreach (GameDTO gameDTO in gamesDTO)
                {
                    Game game = new Game();
                    game.ID = gameDTO.ID;
                    game.Name = gameDTO.Name;
                    game.Developer = gameDTO.Developer;
                    game.YearOfRelease = gameDTO.YearOfRelease;
                    game.Platforms = gameSerializer.DeserializeToEnumOfPlatforms(gameDTO.Platforms);
                    game.Rating = gameDTO.Rating;
                    game.Description = gameDTO.Description;
                    game.Icon = gameDTO.Icon;
                    game.Screenshots = gameSerializer.DeserializeToListOfStrings(gameDTO.Screenshots);
                    
                    string reviewsQuery = @"SELECT * FROM Reviews WHERE GameId = @GameId";
                    List<Review> reviews = connection.Query<Review>(reviewsQuery, new { GameId = gameDTO.ID }, transaction).AsList();
                    game.Reviews = reviews ?? new List<Review>();
                    outputList.Add((T)(object)game);
                }
            }
            else if(typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = "SELECT * FROM Reviews ORDER BY ID";
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
