
using AutoMapper;
using ToDoListFuckThis.Models;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap(); // map 2 chiều
            CreateMap<Users, UpdateUserDto>().ReverseMap();
            
        }
    }
}
