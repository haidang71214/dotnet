
using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.TodoSectionDto;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Mapping
{
    public class SectionMapping : Profile
    {
        public SectionMapping()
        {
            CreateMap<TodoSection, CreateSection>().ForMember(x=>x.Todolists,section=> section.Ignore())
                .ReverseMap();
            CreateMap<UpdateSectionDto, TodoSection>().ReverseMap(); 
            
        }
    }
}
