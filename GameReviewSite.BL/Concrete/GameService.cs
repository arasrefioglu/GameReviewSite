using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameReviewSite.BL.Concrete
{
    public class GameService: IGameService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<GameComment> _commentRepository;
        private readonly IRepository<UserRating> _ratingRepository;
        private readonly IRepository<GameGenre> _genreRepository;

        public GameService(
            IRepository<Game> gameRepository,
            IRepository<GameComment> commentRepository,
            IRepository<UserRating> ratingRepository,
             IRepository<GameGenre> genreRepository
            )
        {
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _ratingRepository = ratingRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _gameRepository.GetAllWithIncludesAsync(g => g.Genre);
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _gameRepository.GetByIdAsync(id);
        }

        public async Task AddGameAsync(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            _gameRepository.Update(game);
            await _gameRepository.SaveAsync();
        }

        public async Task DeleteGameAsync(int gameId)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game != null)
            {
                _gameRepository.RemoveAsync(game);
                await _gameRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<Game>> FindGamesAsync(Expression<Func<Game, bool>> predicate)
        {
            return await _gameRepository.FindAsync(predicate);
        }

        public async Task AddCommentAsync(GameComment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();
        }

        public async Task AddRatingAsync(UserRating rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));
            await _ratingRepository.AddAsync(rating);
            await _ratingRepository.SaveAsync();
        }

        public async Task<IEnumerable<Game>> GetPopularGamesAsync()
        {
            // Popüler oyunları döndüren örnek sorgu
            return await _gameRepository.FindAsync(g => g.Ratings.Average(r => r.Rating) > 4);
        }

        public async Task<IEnumerable<Game>> GetRecentGamesAsync()
        {
            int currentYear = DateTime.Now.Year; // Geçerli yılı al
            return await _gameRepository.FindAsync(g => g.ReleaseYear >= currentYear - 1);
        }

        public async Task<Game> GetGameDetailsAsync(int id)
        {
            // Genre, Ratings ve Comments nesnelerini dahil ediyoruz
            var game = (await _gameRepository.FindAsync(g => g.Id == id)).FirstOrDefault();

            if (game != null)
            {
                game.Genre = (await _genreRepository.FindAsync(g => g.Id == game.GameGenreId)).FirstOrDefault();
                game.Ratings = (await _ratingRepository.FindAsync(r => r.GameId == id)).ToList();
                game.Comments = (await _commentRepository.GetByIncludesAsync(c => c.GameId == id, q=>q.User)).ToList();
            }

            return game;
        }

    }
}
