using AutoMapper;
using UserManager.Entity;
using UserManager.Models.Dto;

namespace UserManager
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Users, UserDto>();
            CreateMap<UserDto,Users>();
            CreateMap<Users, CreateUserDto>().ReverseMap(); // map 2 chiều
            CreateMap<Users, UpdateUserDto>().ReverseMap();
        }
    }
}
