using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse;
using ToDoListFuckThis.Models.Dto.ProjectDto;
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
        // tạo project mới
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> createProject([FromBody] CreateProjectDto createProject) {

            var newProject = _mapper.Map<Projects>(createProject);
            if (createProject.UsersIds?.Any() == true) {
                var getAllUser = await _user.GetAllAsync(user => createProject.UsersIds.Contains(user.Id)); // hiểu thì là lọc obj
                var userHasset = getAllUser.Select(t=> t.Id).ToHashSet(); // lọc id
            // kiểm tra id sai
                var invailidId = createProject.UsersIds.Except(userHasset).ToList();
                if (invailidId.Any()) {
                    return ApiResponse.Fail("Invailid Ids");
                }

                newProject.Users = getAllUser;
            }
            return ApiResponse.Success(newProject);
        }
        // lấy toàn bộ project của 1 user
        [Authorize]
        [HttpGet("project/user")]
        public async Task<ActionResult<ApiResponse>> getProjectByUser() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            var GuidUserId = Guid.Parse(userId);
            var user = await _user.GetAsync(user => user.Id == GuidUserId, includeProperties: "Projects");
            return ApiResponse.Success(user);
        }
    }
}
