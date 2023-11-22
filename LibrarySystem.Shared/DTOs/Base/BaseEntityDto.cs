namespace LibrarySystem.Shared.DTOs.Base
{
    public class BaseEntityDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
