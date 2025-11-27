using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.ToDoListDto;

namespace ToDoListFuckThis.Mapping
{
    public class TodolistMapping : Profile
    {
        public TodolistMapping()
        {
     
            CreateMap<string, DateTimeOffset>()
                .ConvertUsing(src => string.IsNullOrWhiteSpace(src)
                    ? default
                    : DateTimeOffset.Parse(src).ToUniversalTime());

            CreateMap<string, DateTimeOffset?>()
                .ConvertUsing(src => string.IsNullOrWhiteSpace(src)
                    ? null
                    : DateTimeOffset.Parse(src).ToUniversalTime());


            CreateMap<TodolistCreateRequestDto, Todolists>()
    .ForMember(dest => dest.timeStart, opt => opt.MapFrom(src =>
        src.timeStart != null
            ? DateTimeOffset.Parse(src.timeStart).ToUniversalTime()
            : (DateTimeOffset?)null))
    .ForMember(dest => dest.timeEnd, opt => opt.MapFrom(src =>
        src.timeEnd != null
            ? DateTimeOffset.Parse(src.timeEnd).ToUniversalTime()
            : (DateTimeOffset?)null))
    .ForMember(x => x.User, opt => opt.Ignore())
    .ForMember(x => x.Id, opt => opt.Ignore())
    .ForMember(x => x.TodoSectionId, opt => opt.Ignore());

            CreateMap<Todolists, TodolistResponseDto>().ReverseMap();

            CreateMap<TodolistUpdateRequestDto, Todolists>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}