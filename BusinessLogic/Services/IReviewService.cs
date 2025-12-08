using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IReviewService
    {
        /// <summary>
        /// Добавляет отзыв к указанной игре.
        /// </summary>
        /// <param name="gameId">ID игры, к которой добавляется отзыв.</param>
        /// <param name="review">Объект отзыва для добавления.</param>
        /// <returns>true, если отзыв добавлен; иначе false.</returns>
        bool AddReviewToGame(int gameId, Review review);

        /// <summary>
        /// Получает все отзывы для игры по её ID.
        /// </summary>
        /// <param name="gameId">ID игры, для которой запрашиваются отзывы.</param>
        /// <returns>Список отзывов для игры; если игра не найдена, возвращается пустой список.</returns>
        List<Review> GetReviewsByGameId(int gameId);
    }
}
