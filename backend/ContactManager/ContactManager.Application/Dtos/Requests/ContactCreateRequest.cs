using Microsoft.AspNetCore.Http;

namespace ContactManager.Application.Dtos.Requests
{
    public class ContactCreateRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public IFormFile File { get; set; }
    }
}
