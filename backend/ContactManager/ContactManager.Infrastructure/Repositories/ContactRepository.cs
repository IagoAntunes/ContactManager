using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Domain.Result;
using MongoDB.Driver;

namespace ContactManager.Infrastructure.Repositories
{
    internal class ContactRepository : IContactRepository
    {

        private readonly IMongoCollection<ContactEntity> _contactsCollection;
        public ContactRepository(IMongoDatabase mongoDatabase)
        {
            _contactsCollection = mongoDatabase.GetCollection<ContactEntity>("Contacts");
        }
        public async Task<Result<ContactEntity>> Create(ContactEntity contact)
        {
            var existingContact = await _contactsCollection.Find(c => c.Email == contact.Email && c.UserId == contact.UserId).FirstOrDefaultAsync();
            if (existingContact != null)
            {
                return Result<ContactEntity>.Failure(OperationStatus.Conflict, $"Um contato com o email '{contact.Email}' já existe para este usuário.");
            }

            await _contactsCollection.InsertOneAsync(contact);
            return Result<ContactEntity>.Success(contact);
        }

        public async Task<DeleteResultStatus> DeleteAsync(string contactId, string userId)
        {
            try
            {
                var result = await _contactsCollection.DeleteOneAsync(c => c.Id == contactId && c.UserId == userId);

                if (!result.IsAcknowledged)
                {
                    return DeleteResultStatus.Failure;
                }

                if (result.DeletedCount == 1)
                {
                    return DeleteResultStatus.Success;
                }
                else
                {
                    return DeleteResultStatus.NotFound;
                }
            }
            catch (MongoException)
            {
                return DeleteResultStatus.Failure;
            }
        }

        public async Task<Result<IEnumerable<ContactEntity>>> GetAll(string userId)
        {
            var contacts = await _contactsCollection.Find(c => c.UserId == userId).ToListAsync();

            return Result<IEnumerable<ContactEntity>>.Success(contacts);
        }

        public async Task<Result<ContactEntity>> GetById(string contactId, string userId)
        {
            var contact = await _contactsCollection.Find(c => c.Id == contactId).FirstOrDefaultAsync();

            if (contact == null)
            {
                return Result<ContactEntity>.Failure(OperationStatus.NotFound, $"Contato com ID '{contactId}' não foi encontrado.");
            }

            return Result<ContactEntity>.Success(contact);
        }

        public async Task<Result<ContactEntity>> UpdateAsync(ContactEntity contact)
        {
            var filterByEmail = Builders<ContactEntity>.Filter.And(
                 Builders<ContactEntity>.Filter.Eq(c => c.Email, contact.Email),
                 Builders<ContactEntity>.Filter.Eq(c => c.UserId, contact.UserId),
                 Builders<ContactEntity>.Filter.Ne(c => c.Id, contact.Id));

            var existingContactWithEmail = await _contactsCollection.Find(filterByEmail).FirstOrDefaultAsync();
            if (existingContactWithEmail != null)
            {
                return Result<ContactEntity>.Failure(OperationStatus.Conflict, $"O email '{contact.Email}' já está em uso por outro contato.");
            }

            var filter = Builders<ContactEntity>.Filter.And(
                Builders<ContactEntity>.Filter.Eq(c => c.Id, contact.Id),
                Builders<ContactEntity>.Filter.Eq(c => c.UserId, contact.UserId)
            );

            var updateDefinition = Builders<ContactEntity>.Update
                .Set(c => c.Name, contact.Name)
                .Set(c => c.Email, contact.Email)
                .Set(c => c.Description, contact.Description)
                .Set(c => c.Phone, contact.Phone)
                .Set(c => c.ImageId, contact.ImageId);

            var options = new FindOneAndUpdateOptions<ContactEntity>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatedContact = await _contactsCollection.FindOneAndUpdateAsync(filter, updateDefinition, options);

            if (updatedContact == null)
            {
                return Result<ContactEntity>.Failure(OperationStatus.NotFound, $"Contato não encontrado");
            }


            return Result<ContactEntity>.Success(updatedContact);
        }
    }
}
