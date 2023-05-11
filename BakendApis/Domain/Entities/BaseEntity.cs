namespace BackendApis.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
