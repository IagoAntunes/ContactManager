namespace ContactManager.Domain.Dtos
{
    public class ContactDto
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageId { get; set; }
    }
}
