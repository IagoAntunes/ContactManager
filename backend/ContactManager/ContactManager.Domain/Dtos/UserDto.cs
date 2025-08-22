namespace ContactManager.Domain.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        public required string Name { get; set; }

        public required string Email { get; set; }

    }
}
