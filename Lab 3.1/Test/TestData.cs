using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3._1.Test
{
    public static class TestData
    {
        public static List<Game> GenerateSampleGames()
        {
            var games = new List<Game>();

            
            var witcher3 = new Game
            {
                ID = 1,
                Name = "The Witcher 3: Wild Hunt",
                Developer = "CD Projekt Red",
                YearOfRelease = 2015,
                Platforms = new List<EnumPlatforms> { EnumPlatforms.PC, EnumPlatforms.PlayStation, EnumPlatforms.XboxOne },
                Rating = 9.8f,
                Description = "Эпическая RPG по мотивам книг Анджея Сапковского. Игра года 2015.",
                Icon = LoadImage("the-witcher-3-wild-1.jpg"),
                Screenshots = new List<Image>
                {
                    LoadImage("the-witcher-3-wild-2.jpg"),
                    LoadImage("the-witcher-3-wild-3.jpg"),
                    LoadImage("the-witcher-3-wild-4.jpg"),
                    LoadImage("the-witcher-3-wild-5.jpg")
                },
                GamesFromSeries = new List<Game>(),
                Reviews = new List<Review>
                {
                    new Review { Username = "GamerAlex", Rating = 5.0f, ReviewText = "Шедевр всех времён и народов!" },
                    new Review { Username = "RPG_Fan", Rating = 4.5f, ReviewText = "Огромный мир, но сложный интерфейс." },
                    new Review { Username = "CasualPlayer", Rating = 3.0f, ReviewText = "Слишком много текста, не для меня." }
                }
            };

            
            var cyberpunk = new Game
            {
                ID = 2,
                Name = "Cyberpunk 2077",
                Developer = "CD Projekt Red",
                YearOfRelease = 2020,
                Platforms = new List<EnumPlatforms> { EnumPlatforms.PC, EnumPlatforms.PlayStation5, EnumPlatforms.XboxSeries },
                Rating = 7.5f,
                Description = "Футуристический экшен в мире Night City. После патчей игра стала значительно лучше.",
                Icon = LoadImage("Cyberpunk-2077-1.jpg"),
                Screenshots = new List<Image>
                {
                    LoadImage("Cyberpunk-2077-2.png"),
                    LoadImage("Cyberpunk-2077-3.png"),
                    LoadImage("Cyberpunk-2077-4.png")
                },
                GamesFromSeries = new List<Game>(),
                Reviews = new List<Review>
                {
                    new Review { Username = "TechGuru", Rating = 4.0f, ReviewText = "Графика на максимуме — вау!" },
                    new Review { Username = "Disappointed", Rating = 2.0f, ReviewText = "Релиз был катастрофой, до сих пор баги." },
                    new Review { Username = "Optimist", Rating = 5.0f, ReviewText = "После обновлений — совершенно новая игра!" }
                }
            };

            
            var hollowKnight = new Game
            {
                ID = 3,
                Name = "Hollow Knight",
                Developer = "Team Cherry",
                YearOfRelease = 2017,
                Platforms = new List<EnumPlatforms> { EnumPlatforms.PC, EnumPlatforms.PlayStation, EnumPlatforms.XboxOne },
                Rating = 9.5f,
                Description = "Метроидвания с потрясающей атмосферой, музыкой и боевой системой.",
                Icon = LoadImage("Hollow-knight-1.jpeg"),
                Screenshots = new List<Image>
                {
                    LoadImage("Hollow-knight-2.jpg"),
                    LoadImage("Hollow-knight-3.png")
                },
                GamesFromSeries = new List<Game>(),
                Reviews = new List<Review>
                {
                    new Review { Username = "IndieLover", Rating = 5.0f, ReviewText = "Лучшая инди-игра десятилетия!" },
                    new Review { Username = "HardcoreGamer", Rating = 4.5f, ReviewText = "Сложно, но справедливо. Гениальный дизайн." }
                }
            };

            var hollowKnight_silksong = new Game
            {
                ID = 4,
                Name = "Hollow Knight: silksong",
                Developer = "Team Cherry",
                YearOfRelease = 2025,
                Platforms = new List<EnumPlatforms> { EnumPlatforms.PC, EnumPlatforms.PlayStation5, EnumPlatforms.XboxOne, EnumPlatforms.NintendoSwitch },
                Rating = 9.5f,
                Description = "Метроидвания с потрясающей атмосферой, музыкой и боевой системой.",
                Icon = LoadImage("Hollow-knight-silksong-1.png"),
                Screenshots = new List<Image>
                {
                    LoadImage("Hollow-knight-silksong-2.jpeg")
                },
                GamesFromSeries = new List<Game>(),
                Reviews = new List<Review>
                {
                    new Review { Username = "IndieLover", Rating = 5.0f, ReviewText = "Лучшая инди-игра десятилетия!" },
                    new Review { Username = "HardcoreGamer", Rating = 4.5f, ReviewText = "Сложно, но справедливо. Гениальный дизайн." }
                }
            };
            List<Game> games1 = [hollowKnight, hollowKnight_silksong];
            games.Add(witcher3);
            games.Add(cyberpunk);
            games.Add(hollowKnight);
            games.Add(hollowKnight_silksong);

            return games;
        }

        // Вспомогательный метод для загрузки изображений
        private static Image LoadImage(string fileName)
        {
            try
            {
                // Путь к изображениям — предполагаем, что они лежат в папке "Images" рядом с .exe
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "D:\\Projects\\pract1\\pract1\\Pictures", fileName);
                return System.IO.File.Exists(path) ? Image.FromFile(path) : Properties.Resources.No_image;
            }
            catch
            {
                return Properties.Resources.No_image; // заглушка из ресурсов
            }
        }
    }
}
