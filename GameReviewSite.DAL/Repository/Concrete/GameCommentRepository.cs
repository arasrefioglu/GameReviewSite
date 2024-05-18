using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;

public class GameCommentRepository : Repository<GameComment>, IRepository<GameComment>
{
    public GameCommentRepository(GameDbContext context) : base(context)
    {
    }
}