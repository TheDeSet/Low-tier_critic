using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Получает игру по её ID.
        /// </summary>
        /// <param name="id">ID игры.</param>
        /// <returns>Объект игры формата Game.</returns>
        Game GetGameById(int id);
        /// <summary>
        /// Получает полный список игр из репозитория.
        /// </summary>
        /// <returns>Список всех игр в формате Game.</returns>
        List<Game> GetGames();
        /// <summary>
        /// Фильтрует и сортирует игры по заданным параметрам.
        /// </summary>
        /// <param name="searchField">Поле для поиска (названию, разработчику, искать по всему).</param>
        /// <param name="searchText">Текст для поиска.</param>
        /// <param name="sortOption">Опция сортировки (возрастанию (рейтинг), убыванию (рейтинг)).</param>
        /// <returns>Отфильтрованный и отсортированный список игр.</returns>
        List<Game> GetFilteredGames(string searchField, string searchText, string sortOption);
        /// <summary>
        /// Добавляет новую игру в репозиторий.
        /// </summary>
        /// <param name="game">Объект игры для добавления.</param>
        /// <remarks>
        /// Автоматически назначает ID (максимальный текущий +1), инициализирует пустые списки (Platforms, Screenshots, Reviews) при необходимости.
        /// </remarks>
        void AddGame(Game game);
        /// <summary>
        /// Обновляет существующую игру по ID.
        /// </summary>
        /// <param name="updatedGame">Обновленный объект игры.</param>
        /// <returns>true, если игра обновлена; иначе false.</returns>
        bool UpdateGame(Game updatedGame);
        /// <summary>
        /// Удаляет игру по указанному ID из репозитория.
        /// </summary>
        /// <param name="gameId">ID удаляемой игры.</param>
        /// <returns>true, если игра удалена; иначе false.</returns>
        bool DeleteGame(int gameId);
    }
}
