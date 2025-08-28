using AutoMapper;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Mapping
{
    internal class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<ContactEntity, ContactDto>();
            CreateMap<ImageMetadataEntity, ImageDto>();
        }
    }
}
