namespace LibrarySystem.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
