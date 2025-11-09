
using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.ToDoListDto;

namespace ToDoListFuckThis.Mapping
{
    public class TodolistMapping : Profile
    {
        public TodolistMapping()
        {
            CreateMap<TodolistCreateRequestDto, Todolists>()
                // bỏ cái user đi được rồi
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.TodoSectionId , opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Todolists, TodolistResponseDto>().ReverseMap();
            CreateMap<TodolistUpdateRequestDto,Todolists>().ReverseMap();
        }
    }
}
