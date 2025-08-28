using ContactManager.Application.Dtos.Requests;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Result;

namespace ContactManager.Application.Services
{
    public interface IContactService
    {
        Task<Result<ContactDto>> CreateAsync(ContactCreateRequest createContact, string userId);
        Task<Result<ContactDto>> UpdateAsync(ContactUpdateRequest updateContact, string contactId, string userId);
        Task<DeleteResultStatus> DeleteAsync(string contactId, string userId);
        Task<Result<IEnumerable<ContactDto>>> GetAll(string userId);
        Task<Result<ContactDto>> GetById(string contactId, string userId);
    }
}
