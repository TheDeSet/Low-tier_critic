using Dapper;
using Entities;
using Microsoft.Data.Sqlite;
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
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer
{
    public class DapperRepository <T> : IRepository<T> where T : IDomainObject
    {
        private class GameDTO
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

        readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Projects\\Homework\\C#\\Lab 3.1\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security=True";

        private string SerializePlatforms(List<EnumPlatforms> platforms)
        {
            string outputString = "";
            foreach (Enum platform in Enum.GetValues<Entities.EnumPlatforms>())
            {
                FieldInfo field = platform.GetType().GetField(platform.ToString());
                DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
                outputString += $"{attribute.Description};";
            }
            return outputString;
        }

        private string SerializeScreenshots(List<string> images)
        {
            string outputString = "";
            foreach (string image in images)
            {
                outputString += $"{image};";
            }
            return outputString;
        }

        private string SerializeReviews(List<Review> reviews)
        {
            string outputString = "";
            foreach (Review review in reviews)
            {
                outputString += $"{review.ID};";
            }
            return outputString;
        }

        private List<T> DeserializePlatforms<T>(string platforms) where T : Enum
        {
            List<string> descriptions = platforms.Split(';').ToList();
            return descriptions.Select(d =>
                Enum.GetValues(typeof(T))
                    .Cast<T>()
                    .First(c =>
                        (typeof(T).GetField(c.ToString())?.GetCustomAttribute<DescriptionAttribute>()?.Description == d)
                        || c.ToString() == d
                    )
            ).ToList();
        }

        private List<string> DeserializeScreenshots(string screenshotsString)
        {
            if (screenshotsString == null)
            {
                return new List<string>();
            }
            else
            {
                return screenshotsString.Split(";").ToList();
            }   
        }

        private List<Review> DeserializeReviews(string reviewsString)
        {
            List<Review> reviews = new List<Review>();
            List<string> reviewsIdsStrings = reviewsString.Split(";").ToList();
            List<int> reviewsIds = new List<int>();
            foreach (string reviewId in reviewsIdsStrings)
            {
                if (int.TryParse(reviewId, out int id))
                {
                    reviewsIds.Add(id);
                }
            }
            foreach (int reviewId in reviewsIds)
            {
                reviews.Add(ReadById(reviewId) as Review);
            }
            return reviews;
        }

        public void Add(T entity)
        {   
            if (typeof(T) == typeof(Entities.Game))
            {
                Game game = entity as Game;
                string sqlQuery = @"INSERT INTO Games (ID, Name, Developer, YearOfRelease, Platforms, Rating, Description, Icon, Screenshots, Reviews) " +
                    "VALUES (@ID, @Name, @Developer, @YearOfRelease, @Platforms, @Rating, @Description, @Icon, @Screenshots, @Reviews)";
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
                    Reviews = SerializeReviews(game.Reviews)
                };
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, parameters);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"INSERT INTO Reviews (ID, Username, Rating, ReviewText) " +
                    "VALUES (@ID, @Username, @Rating, @ReviewText)";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, review);
            }
        }

        public void Delete<T>(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                string sqlQuery = @"DELETE FROM Games WHERE ID = @ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, new { ID = id });
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = @"DELETE FROM Reviews WHERE ID = @ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, new { ID = id });
            }
        }

        public void Update(T entity)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                Game game = entity as Game;
                game.Platforms ??= new List<EnumPlatforms>();
                game.Screenshots ??= new List<string>();
                game.Reviews ??= new List<Review>();
                string sqlQuery = @"UPDATE Games" +
                    "SET Name = @Name, Developer = @Developer, YearOfRelease = @YearOfRelease, Platforms = @Platforms, Rating = @Rating, Description = @Description, Icon = @Icon, Screenshots = @Screenshots, Reviews = @Reviews " +
                    "WHERE ID = @ID";
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
                    Reviews = SerializeReviews(game.Reviews)
                };
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, parameters);
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                Review review = entity as Review;
                string sqlQuery = @"UPDATE Reviews " +
                    "SET Username = @Username, Rating = @Rating, ReviewText = @ReviewText " +
                    "WHERE ID = @ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                connection.Execute(sqlQuery, review);
            }
        }

        public T? ReadById(int id)
        {
            if (typeof(T) == typeof(Entities.Game))
            {
                string sqlQuery = @"SELECT * FROM Games WHERE ID = @ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                GameDTO gameDTO = connection.QueryFirstOrDefault<GameDTO>(sqlQuery, new { ID = id });
                Game game = new Game();
                game.ID = gameDTO.ID;
                game.Name = gameDTO.Name;
                game.Developer = gameDTO.Developer;
                game.YearOfRelease = gameDTO.YearOfRelease;
                game.Platforms = DeserializePlatforms<EnumPlatforms>(gameDTO.Platforms);
                game.Rating = gameDTO.Rating;
                game.Description = gameDTO.Description;
                game.Icon = gameDTO.Icon;
                game.Screenshots = DeserializeScreenshots(gameDTO.Screenshots);
                game.Reviews = DeserializeReviews(gameDTO.Reviews);
                return (T)(object)game;
            }
            else if (typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = @"SELECT * FROM Reviews WHERE ID = @ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                Review review = connection.QueryFirstOrDefault<Review>(sqlQuery, new { ID = id });
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
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                List<GameDTO> gamesDTO = connection.Query<GameDTO>(sqlQuery).AsList();
                foreach (GameDTO gameDTO in gamesDTO)
                {
                    Game game = new Game();
                    game.ID = gameDTO.ID;
                    game.Name = gameDTO.Name;
                    game.Developer = gameDTO.Developer;
                    game.YearOfRelease = gameDTO.YearOfRelease;
                    game.Platforms = DeserializePlatforms<EnumPlatforms>(gameDTO.Platforms);
                    game.Rating = gameDTO.Rating;
                    game.Description = gameDTO.Description;
                    game.Icon = gameDTO.Icon;
                    game.Screenshots = DeserializeScreenshots(gameDTO.Screenshots);
                    game.Reviews = DeserializeReviews(gameDTO.Reviews);
                    outputList.Add((T)(object)game);
                }
            }
            else if(typeof(T) == typeof(Entities.Review))
            {
                string sqlQuery = "SELECT * FROM Reviews ORDER BY ID";
                using IDbConnection connection = new SqliteConnection(ConnectionString);
                List<Review> reviews = connection.Query<Review>(sqlQuery).AsList();
                foreach (Review review in reviews)
                {
                    outputList.Add((T)(object)review);
                }
            }
            return outputList;
        }
    }
}
