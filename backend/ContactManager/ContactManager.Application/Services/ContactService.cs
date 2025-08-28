using AutoMapper;
using ContactManager.Application.Dtos.Requests;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Domain.Result;
using MongoDB.Bson;

namespace ContactManager.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public ContactService(
            IContactRepository contactRepository, 
            IMapper mapper,
            IImageService imageService
            )
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<Result<ContactDto>> CreateAsync(ContactCreateRequest createContact, string userId)
        {
            var imageCreated = await imageService.SaveImage(createContact.File, userId);

            var contactToCreate = new ContactEntity {
                Name = createContact.Name,
                Email = createContact.Email,
                Description = createContact.Description,
                Phone = createContact.Phone,
                UserId = userId,
                ImageId = imageCreated.Id,
            };
            var resultCreateContact = await contactRepository.Create(contactToCreate);
            var finalResult = resultCreateContact.Map(contactEntity => mapper.Map<ContactDto>(contactEntity));

            return finalResult;
        }

        public async Task<Result<ContactDto>> UpdateAsync(ContactUpdateRequest updateContactRequest, string contactId, string userId)
        {
            var existingContactResult = await contactRepository.GetById(contactId, userId);
            if (existingContactResult.IsFailure)
            {
                return Result<ContactDto>.Failure(existingContactResult.Status, existingContactResult.Error);
            }
            var contactToUpdate = existingContactResult.Value;
            mapper.Map(updateContactRequest, contactToUpdate);
            if (updateContactRequest.File != null)
            {
                var oldImageId = contactToUpdate.ImageId;

                var newImageResult = await imageService.SaveImage(updateContactRequest.File, userId);
                if (newImageResult != null)
                {
                    contactToUpdate.ImageId = newImageResult.Id;
                }

                if (!string.IsNullOrEmpty(oldImageId))
                {
                    await imageService.DeleteImage(oldImageId);
                }

            }

            var resultUpdateContact = await contactRepository.UpdateAsync(contactToUpdate);

            var finalResult = resultUpdateContact.Map(updatedEntity => mapper.Map<ContactDto>(updatedEntity));

            return finalResult;
        }

        public async Task<DeleteResultStatus> DeleteAsync(string contactId, string userId)
        {
            var deleteResult = await contactRepository.DeleteAsync(contactId, userId);
            return deleteResult;
        }

        public async Task<Result<IEnumerable<ContactDto>>> GetAll(string userId)
        {
            var resultGetAllContact = await contactRepository.GetAll(userId);
            var finalResult = resultGetAllContact.Map(contactEntity => mapper.Map<IEnumerable<ContactDto>>(contactEntity));

            return finalResult;
        }

        public async Task<Result<ContactDto>> GetById(string contactId, string userId)
        {
            var resultGetByIdContact = await contactRepository.GetById(contactId, userId);
            var finalResult = resultGetByIdContact.Map(contactEntity => mapper.Map<ContactDto>(contactEntity));

            return finalResult;
        }


    }
}
