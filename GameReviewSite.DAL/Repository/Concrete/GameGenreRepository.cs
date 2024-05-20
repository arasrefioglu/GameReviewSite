using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;

public class GameGenreRepository : Repository<GameGenre>, IRepository<GameGenre>
{
    public GameGenreRepository(GameDbContext context) : base(context)
    {
    }
}
