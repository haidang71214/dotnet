
using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Mapping
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            // bỏ cái UserIds với TodoSectionIds đi, với id của project nữa
            CreateMap<ProjectRequest, Projects>()
     .ForMember(x => x.Id, opt => opt.Ignore())
     .ForMember(dest => dest.Users, opt => opt.Ignore())
     .ForMember(dest => dest.Todosection, opt => opt.Ignore())
     .ReverseMap();
            CreateMap<Projects, ProjectResponse>().ReverseMap();
            
        }
    }
}
