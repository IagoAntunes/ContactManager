namespace ContactManager.Domain.Dtos
{
    public class ImageDto
    {
        public string Id { get; set; } = string.Empty;
        public required string OriginalFileName { get; set; }
        public required string StoredFileName { get; set; }
        public required string StoredPath { get; set; }
        public required string ContentType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
