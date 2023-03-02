namespace Services.Data.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
