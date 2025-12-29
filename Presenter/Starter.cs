using BusinessLogic;
using BusinessLogic.Services;
using Ninject;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class Starter
    {
        private static IKernel kernel;
        /// <summary>
        /// Инициализирует контейнер зависимостей
        /// </summary>
        /// <param name="useEF">true для использования Entity Framework, false для Dapper</param>
        public static void Initialize(bool useEF)
        {
            kernel = new StandardKernel(new NinjectConfigModule(useEF));
        }
        /// <summary>
        /// Создает главное меню с всеми зависимостями
        /// </summary>
        /// <returns>Экземпляр главного меню</returns>
        public static IFormMainMenu CreateMainMenu()
        {
            var view = new pract1.MainMenu();
            var gameService = kernel.Get<IGameService>();
            var reviewService = kernel.Get<IReviewService>();
            var presenter = new MainMenuPresenter(view, gameService, reviewService);
            return view;
        }
        /// <summary>
        /// Создает представление деталей игры с зависимостями
        /// </summary>
        /// <param name="gameId">ID игры для отображения</param>
        /// <returns>Экземпляр представления игры</returns>
        //public static IGameDetailsView CreateGameDetailsView(int gameId)
        //{
        //    var view = new View.FullGameInformation(gameId);
        //    var gameService = kernel.Get<IGameService>();
        //    var reviewService = kernel.Get<IReviewService>();
        //    var presenter = new GameDetailsPresenter(view, gameId, gameService, reviewService);
        //    return view;
        //}
        /// <summary>
        /// Создает форму добавления новой игры
        /// </summary>
        /// <returns>Экземпляр формы добавления игры</returns>
        public static IAddGameView CreateAddGameView()
        {
            var view = new View.AddNewGame();
            var gameService = kernel.Get<IGameService>();
            var presenter = new AddGamePresenter(view, gameService);
            return view;
        }
        /// <summary>
        /// Создает форму добавления отзыва
        /// </summary>
        /// <param name="gameId">ID игры, для которой добавляется отзыв</param>
        /// <returns>Экземпляр формы добавления отзыва</returns>
        public static IAddReviewView CreateAddReviewView(int gameId)
        {
            var view = new View.AddReview(gameId);
            var reviewService = kernel.Get<IReviewService>();
            var presenter = new AddReviewPresenter(view, gameId, reviewService);
            return view;
        }
        /// <summary>
        /// Создает форму обновления игры
        /// </summary>
        /// <param name="gameId">ID игры для обновления</param>
        /// <returns>Экземпляр формы обновления игры</returns>
        public static IUpdateGameView CreateUpdateGameView(int gameId)
        {
            var view = new View.UpdateGame(gameId);
            var gameService = kernel.Get<IGameService>();
            var presenter = new UpdateGamePresenter(view, gameId, gameService);
            return view;
        }
    }
}
