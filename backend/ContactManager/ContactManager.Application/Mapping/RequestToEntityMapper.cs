using AutoMapper;
using ContactManager.Application.Dtos.Requests;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Mapping
{
    public class RequestToEntityMapper : Profile
    {
        public RequestToEntityMapper()
        {

            CreateMap<ContactUpdateRequest, ContactEntity>();
        }
    }
}
