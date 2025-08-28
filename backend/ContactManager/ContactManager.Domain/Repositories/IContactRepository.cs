using ContactManager.Domain.Entities;
using ContactManager.Domain.Result;

namespace ContactManager.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<Result<ContactEntity>> Create(ContactEntity contact);
        Task<Result<ContactEntity>> UpdateAsync(ContactEntity contact);
        Task<DeleteResultStatus> DeleteAsync(string contactId, string userId);
        Task<Result<IEnumerable<ContactEntity>>> GetAll(string userId);
        Task<Result<ContactEntity>> GetById(string contactId, string userId);
    }
}
