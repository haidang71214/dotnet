
using AutoMapper;
using ToDoListFuckThis.Models;
using UserManager.Models.Dto;

namespace ToDoListFuckThis
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap(); // map 2 chiều
            CreateMap<Users, UpdateUserDto>().ReverseMap();
        }
    }
}
