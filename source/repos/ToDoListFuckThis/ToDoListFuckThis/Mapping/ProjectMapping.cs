using AutoMapper;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.ProjectDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;

namespace ToDoListFuckThis.Mapping
{
    public class ProjectMapping :Profile
    {
        public ProjectMapping()
        {
            CreateMap<CreateProjectDto,Projects>().ReverseMap();
        }
    }
}
