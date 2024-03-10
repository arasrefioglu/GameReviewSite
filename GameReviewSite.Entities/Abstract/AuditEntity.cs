namespace GameReviewSite.Entities.Abstract
{
    public abstract class AuditEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
