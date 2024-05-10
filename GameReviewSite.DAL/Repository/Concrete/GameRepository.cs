using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;
using GameReviewSite.Repositories;

public class GameRepository : Repository<Game>, IRepository<Game>
{
    public GameRepository(GameDbContext context) : base(context)
    {
    }
}
