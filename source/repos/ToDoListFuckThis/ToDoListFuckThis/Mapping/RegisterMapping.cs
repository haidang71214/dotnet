using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.RegisterDto;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Mapping
{
    public class RegisterMapping : Profile
    {
        public RegisterMapping()
        {
            CreateMap<RegisterRequest, Users>().ReverseMap();
            CreateMap<Users, RegisterResponseDto>().ReverseMap();   
        }
    }
}
