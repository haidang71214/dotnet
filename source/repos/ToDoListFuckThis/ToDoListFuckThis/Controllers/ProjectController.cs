using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.repository.IRepository;

namespace ToDoListFuckThis.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _project;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository project, IMapper mapper, IUserRepository user) { 
            _project = project;
            _mapper = mapper;
            _user = user;
        }
        // xem list project của chính bản thân mình

        // crud cái project đó
        [HttpGet]
        public async Task<ActionResult<ApiListResponse<Projects>>> getAllProject() {

            return ApiListResponse<Projects>.Success(await _project.GetAllAsync());
        }
        [HttpGet("project/{id}")]
        // lấy chi tiết của thằng project ra (nhưng không lấy section, vì section id sẽ do thằng section làm)
        public async Task<ActionResult<ApiResponse>> getAllSectionInProject(Guid id) { 
            var projectList = await _project.GetAsync(p => p.Id == id);
            return ApiResponse.Success(projectList);
        }

        // tạo cái section rồi add vào đây nữa
    }
}
