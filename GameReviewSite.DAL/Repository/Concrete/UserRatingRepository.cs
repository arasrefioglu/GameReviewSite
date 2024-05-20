using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;

public class UserRatingRepository : Repository<UserRating>, IRepository<UserRating>
{
    public UserRatingRepository(GameDbContext context) : base(context)
    {
    }
}