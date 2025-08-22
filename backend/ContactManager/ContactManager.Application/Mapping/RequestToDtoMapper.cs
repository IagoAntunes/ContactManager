using AutoMapper;
using ContactManager.API.Dtos.Requests;
using ContactManager.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.Mapping
{
    public class RequestToDtoMapper : Profile
    {

        public RequestToDtoMapper()
        {
            CreateMap<AuthRegisterRequest, AuthRegisterDto>();
            CreateMap<AuthLoginRequest, AuthLoginDto>();
        }
    }
}
