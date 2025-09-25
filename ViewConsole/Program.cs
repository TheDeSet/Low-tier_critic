using Lab_3._1;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Text;

namespace ViewConsole
{
    internal class Program
    {
        /// <summary>
        /// Возвращает список строковых описаний всех платформ из перечисления EnumPlatforms.
        /// </summary>
        /// <returns>Список строк с описаниями платформ.</returns>
        public static List<string> GetPlatformsStringList()
        {
            List<string> listOfPlatforms = new List<string>();
            foreach (Enum platform in Enum.GetValues<EnumPlatforms>())
            {
                FieldInfo field = platform.GetType().GetField(platform.ToString());
                DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
                listOfPlatforms.Add(attribute.Description);
            }
            return listOfPlatforms;
        }

        /// <summary>
        /// Возвращает список строковых описаний платформ для указанной игры.
        /// </summary>
        /// <param name="game">Игра, для которой получить платформы.</param>
        /// <returns>Список строк с описаниями платформ игры.</returns>
        public static List<string> GetPlatformsStringList(Game game)
        {
            List<string> listOfPlatforms = new List<string>();
            foreach (Enum platform in game.Platforms)
            {
                FieldInfo field = platform.GetType().GetField(platform.ToString());
                DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
                listOfPlatforms.Add(attribute.Description);
            }
            return listOfPlatforms;
        }

        /// <summary>
        /// Преобразует список строковых описаний в соответствующие значения перечисления.
        /// </summary>
        /// <typeparam name="T">Тип перечисления для преобразования.</typeparam>
        /// <param name="descriptions">Список строковых описаний для преобразования.</param>
        /// <returns>Список значений перечисления T, соответствующих описаниям.</returns>
        public static List<T> FromDescriptionsToEnum<T>(IEnumerable<string> descriptions) where T : Enum
        {
            return descriptions.Select(d =>
                Enum.GetValues(typeof(T))
                    .Cast<T>()
                    .First(c =>
                        (typeof(T).GetField(c.ToString())?.GetCustomAttribute<DescriptionAttribute>()?.Description == d)
                        || c.ToString() == d
                    )
            ).ToList();
        }

        /// <summary>
        /// Отображает главное меню приложения с опциями выбора игры, управления списком, поиска/сортировки, сброса сортировки и выхода.
        /// </summary>
        static void PrintMainMenu()
        {
            List<Game> listOfGames = Logic.GetGames();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Выбрать игру из списка\n2. Меню управления списком\n3. Поиск/Сортировка\n4. Сброс сортировки\n5. Выход");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        PrintGameSelect(listOfGames, 0);
                        break;
                    case '2':
                        PrintControlMenu(listOfGames);
                        break;
                    case '3':
                        listOfGames = PrintSearchAndFilterMenu();
                        break;
                    case '4':
                        listOfGames = Logic.GetGames();
                        break;
                    case '5':
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Отображает список игр и позволяет выбрать игру для просмотра, изменения или удаления.
        /// </summary>
        /// <param name="listOfGames">Список игр для отображения.</param>
        /// <param name="mode">Режим действия: 0 - просмотр игр, 1 - изменение данных, 2 - удаление.</param>
        static void PrintGameSelect(List<Game> listOfGames, int mode)//0=inspect 1=modify 2=delete
        {
            int index = 1;
            while (true)
            {
                Console.Clear();
                index = 1;
                foreach (Game game in listOfGames)
                {
                    Console.WriteLine($"{index}. {game.Name} -- {game.Developer}");
                    index++;
                }
                Console.WriteLine($"{index}. Назад");
                string bufferLine = Console.ReadLine();
                if (Int32.TryParse(bufferLine, out int inputInt))
                {
                    if (inputInt > 0 && inputInt < listOfGames.Count + 2)
                    {
                        if (!(inputInt > listOfGames.Count))
                        {
                            if (mode == 0)
                            {
                                PrintGameDetails(listOfGames[inputInt - 1].ID);
                            }
                            else if (mode == 1)
                            {
                                PrintModifyMenu(listOfGames[inputInt - 1].ID);
                            }
                            else if (mode == 2)
                            {
                                PrintDeletionMenu(listOfGames[inputInt - 1].ID);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Отображает детали игры по указанному ID, включая платформы, описание, рейтинги и отзывы.
        /// </summary>
        /// <param name="id">ID игры для отображения.</param>
        static void PrintGameDetails(int id)
        {
            Game game = Logic.GetGameById(id);
            string gamePlatforms = "";
            foreach (string platform in GetPlatformsStringList(game))
            {
                gamePlatforms += platform + "   ";
            }
            gamePlatforms.Trim();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{game.Name}\n{game.YearOfRelease}\n{game.Developer}\nРейтинг: {game.Rating}\n{gamePlatforms}\n\n{game.Description}\n\nОтзывы:");
                int reviewNumber = 1;
                foreach (Review review in game.Reviews)
                {
                    Console.WriteLine($"{reviewNumber}. {review.Username}\nРейтинг: {review.Rating}\n{review.ReviewText}\n\n\n");
                    reviewNumber++;
                }
                Console.WriteLine("1. Написать отзыв\n2. Назад");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        MakeAReview(id);
                        break;
                    case '2':
                        return;
                }
            }
        }

        /// <summary>
        /// Позволяет пользователю создать отзыв для игры.
        /// </summary>
        /// <param name="id">ID игры, для которой создается отзыв.</param>
        static void MakeAReview(int id)
        {
            Console.Clear();
            Review review = new Review();
            Console.WriteLine("Напишите имя пользователся (или оставьте пустым для анонимной публикации)");
            string inputString = Console.ReadLine();
            if (inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' ')))
            {
                review.Username = null;
            }
            else
            {
                review.Username = inputString;
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Поставьте рейтинг от 1 до 5");
                inputString = Console.ReadLine();
                if (float.TryParse(inputString, out float ratingFloat))
                {
                    review.Rating = ratingFloat;
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine("Напишите текст для отзыва");
            review.ReviewText = Console.ReadLine();
            Logic.AddReviewToGame(id, review);
        }

        /// <summary>
        /// Отображает меню управления списком игр (добавление, изменение, удаление).
        /// </summary>
        /// <param name="listOfGames">Список игр для управления.</param>
        static void PrintControlMenu(List<Game> listOfGames)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Добавить игру\n2. Изменить данные об игре\n3. Удалить игру\n4. Назад");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        PrintAddGameMenu();
                        break;
                    case '2':
                        PrintGameSelect(listOfGames, 1);
                        break;
                    case '3':
                        PrintGameSelect(listOfGames, 2);
                        break;
                    case '4':
                        return;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя данные для добавления новой игры и сохраняет её через логику.
        /// </summary>
        static void PrintAddGameMenu()
        {
            Game game = new Game();
            string inputString = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите название игры");
                inputString = Console.ReadLine();
                if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                {
                    game.Name = inputString;
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите разработчика игры");
                inputString = Console.ReadLine();
                if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                {
                    game.Developer = inputString;
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите год выхода игры");
                inputString = Console.ReadLine();
                if (inputString.Length < 5 && Int32.TryParse(inputString, out int year))
                {
                    game.YearOfRelease = year;
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите описание игры");
                inputString = Console.ReadLine();
                if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                {
                    game.Description = inputString;
                    break;
                }
            }
            int chosenPlatforms = 0;
            List<string> listOfPlatforms = GetPlatformsStringList();
            List<string> listOfChsnPlat = new List<string>();
            while (true)
            {
                Console.Clear();
                if (listOfPlatforms.Count == 0)
                {
                    break;
                }
                int index = 1;
                Console.WriteLine("Выберите платформы, на которых есть игра");
                foreach (string platform in listOfPlatforms)
                {
                    Console.WriteLine($"{index}. {platform}");
                    index++;
                }
                if (chosenPlatforms > 0)
                {
                    Console.WriteLine($"{index}. Готово");
                }
                string line = Console.ReadLine();
                if (Int32.TryParse(line, out int bufferInt))
                {
                    if (bufferInt > 0 && bufferInt < listOfPlatforms.Count + 1)
                    {
                        listOfChsnPlat.Add(listOfPlatforms[bufferInt - 1]);
                        listOfPlatforms.Remove(listOfPlatforms[bufferInt - 1]);
                        chosenPlatforms++;
                    }
                    else if (chosenPlatforms > 0 && bufferInt == listOfPlatforms.Count + 1)
                    {
                        break;
                    }
                }
            }
            game.Platforms = FromDescriptionsToEnum<EnumPlatforms>(listOfChsnPlat);
            Logic.AddGame(game);
        }

        /// <summary>
        /// Отображает меню для изменения данных существующей игры.
        /// </summary>
        /// <param name="id">ID игры для изменения.</param>
        static void PrintModifyMenu(int id)
        {
            Game game = new Game();
            Game gameOld = Logic.GetGameById(id);
            game.ID = gameOld.ID;
            game.Name = gameOld.Name;
            game.Developer = gameOld.Developer;
            game.YearOfRelease = gameOld.YearOfRelease;
            game.Platforms = gameOld.Platforms;
            game.Rating = gameOld.Rating;
            game.Description = gameOld.Description;
            game.Icon = gameOld.Icon;
            game.Screenshots = gameOld.Screenshots;
            game.Reviews = gameOld.Reviews;
            bool isContinuing = true;
            while (isContinuing)
            {
                Console.Clear();
                Console.WriteLine("1. Изменить название\n2. Изменить разработчика\n3. Изменить год выпуска\n4. Измнить описание\n5. Изменить платформы \n6. Назад");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        string inputString = "";
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите название игры");
                            inputString = Console.ReadLine();
                            if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                            {
                                game.Name = inputString;
                                break;
                            }
                        }
                        break;
                    case '2':
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите разработчика игры");
                            inputString = Console.ReadLine();
                            if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                            {
                                game.Developer = inputString;
                                break;
                            }
                        }
                        break;
                    case '3':
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите год выхода игры");
                            inputString = Console.ReadLine();
                            if (inputString.Length < 5 && Int32.TryParse(inputString, out int year))
                            {
                                game.YearOfRelease = year;
                                break;
                            }
                        }
                        break;
                    case '4':
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите описание игры");
                            inputString = Console.ReadLine();
                            if (!(inputString == "" || (inputString.Length > 0 && inputString.All(c => c == ' '))))
                            {
                                game.Description = inputString;
                                break;
                            }
                        }
                        break;
                    case '5':
                        int chosenPlatforms = 0;
                        int inputInt = 0;
                        List<string> listOfPlatforms = GetPlatformsStringList();
                        List<string> listOfChsnPlat = new List<string>();
                        while (true)
                        {
                            Console.Clear();
                            if (listOfPlatforms.Count == 0)
                            {
                                break;
                            }
                            int index = 1;
                            Console.WriteLine("Выберите платформы, на которых есть игра");
                            foreach (string platform in listOfPlatforms)
                            {
                                Console.WriteLine($"{index}. {platform}");
                                index++;
                            }
                            if (chosenPlatforms > 0)
                            {
                                Console.WriteLine($"{index}. Готово");
                            }
                            string line = Console.ReadLine();
                            if (Int32.TryParse(line, out int bufferInt))
                            {
                                if (bufferInt > 0 && bufferInt < listOfPlatforms.Count + 1)
                                {
                                    listOfChsnPlat.Add(listOfPlatforms[bufferInt - 1]);
                                    listOfPlatforms.Remove(listOfPlatforms[bufferInt - 1]);
                                    chosenPlatforms++;
                                }
                                else if (chosenPlatforms > 0 && bufferInt == listOfPlatforms.Count + 1)
                                {
                                    game.Platforms = FromDescriptionsToEnum<EnumPlatforms>(listOfChsnPlat);
                                    break;
                                }
                            }
                        }
                        break;
                    case '6':
                        {
                            Logic.UpdateGame(game);
                            isContinuing = false;
                            break;
                        }            
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Подтверждает удаление игры с указанным ID.
        /// </summary>
        /// <param name="id">ID игры для удаления.</param>
        static void PrintDeletionMenu(int id)
        {
            Game game = Logic.GetGameById(id);
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Вы уверены, что хотите удалить {game.Name}?\n1. Да\n2. Нет");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Logic.DeleteGame(game.ID);
                        return;
                    case '2':
                        return;
                }
            }
        }

        /// <summary>
        /// Отображает меню для поиска и сортировки игр и возвращает отфильтрованный список.
        /// </summary>
        /// <returns>Список игр, отфильтрованных по заданным критериям.</returns>
        static List<Game> PrintSearchAndFilterMenu()
        {
            string searchField = "искать по всему";
            string? searchText = null;
            string sortOption = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Выбрать по чему искать\n2. Вписать текст для поиска\n3. Опции сортировки\n4. Применить");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        bool isContinuing = true;
                        while (isContinuing)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Названию\n2. Разработчику\n3. По всему");
                            var key2 = Console.ReadKey(true);
                            switch (key.KeyChar)
                            {
                                case '1':
                                    searchField = "названию";
                                    isContinuing = false;
                                    break;
                                case '2':
                                    searchField = "разработчику";
                                    isContinuing = false;
                                    break;
                                case '3':
                                    searchField = "искать по всему";
                                    isContinuing = false;
                                    break;
                            }
                        }
                        break;
                    case '2':
                        string bufferString = "";
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите текст для поиска");
                            bufferString = Console.ReadLine();
                            if (!(bufferString == "" || (bufferString.Length > 0 && bufferString.All(c => c == ' '))))
                            {
                                searchText = bufferString;
                                break;
                            }
                        }
                        break;
                    case '3':
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Возрастанию рейтинга\n2. Убыванию рейтинга");
                            var key2 = Console.ReadKey(true);
                            switch (key.KeyChar)
                            {
                                case '1':
                                    sortOption = "возрастанию (рейтинг)";
                                    break;
                                case '2':
                                    sortOption = "убыванию (рейтинг)";
                                    break;
                            }
                        }
                        break;
                    case '4':
                        return Logic.GetFilteredGames(searchField, searchText, sortOption);
                        break;
                    default:
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            PrintMainMenu();
        }
    }

}
