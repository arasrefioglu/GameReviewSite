using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;
using GameReviewSite.Repositories;

public class GameCommentRepository : Repository<GameComment>, IRepository<GameComment>
{
    public GameCommentRepository(GameDbContext context) : base(context)
    {
    }
}