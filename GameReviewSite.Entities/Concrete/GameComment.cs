using GameReviewSite.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Entities.Concrete
{
    public class GameComment : AuditEntity
    {

        public string Comment { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
