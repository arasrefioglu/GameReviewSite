using GameReviewSite.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameReviewSite.BL.Abstract
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int gameId);
        Task<IEnumerable<Game>> FindGamesAsync(Expression<Func<Game, bool>> predicate);

        Task AddCommentAsync(GameComment comment);
        Task AddRatingAsync(UserRating rating);


        Task<Game> GetGameDetailsAsync(int id);
    }
}
