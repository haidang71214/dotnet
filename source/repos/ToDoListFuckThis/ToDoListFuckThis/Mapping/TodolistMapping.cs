
using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.ToDoListDto;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Mapping
{
    public class TodolistMapping : Profile
    {
        public TodolistMapping()
        {
            CreateMap<TodolistRequestDto, Todolists>()
                // bỏ cái user đi được rồi
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Todolists, TodolistResponseDto>().ReverseMap();
            
        }
    }
}
