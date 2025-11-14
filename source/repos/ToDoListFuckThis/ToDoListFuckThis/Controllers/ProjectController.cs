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
using static System.Collections.Specialized.BitVector32;

namespace ToDoListFuckThis.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _project;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _section;
        private readonly ITodoRepository _todo;
        public ProjectController(IProjectRepository project, IMapper mapper, IUserRepository user,ISectionRepository section, ITodoRepository todo) { 
            _project = project;
            _mapper = mapper;
            _user = user;
            _section = section;
            _todo = todo;
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
        [Authorize]
        public async Task<ActionResult<ApiResponse>> createProject([FromBody] CreateProjectDto createProject) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // mình lưu cái này đang là id, nên lôi ra id á mà

            var userIdString = Guid.Parse(userId);
            var newProject = _mapper.Map<Projects>(createProject);
            if (createProject.UsersIds?.Any() == true) {
                var getAllUser = await _user.GetAllAsync(user => createProject.UsersIds.Contains(user.Id)); // hiểu thì là lọc obj
                var userHasset = getAllUser.Select(t=> t.Id).ToHashSet(); // lọc id
            // kiểm tra id sai
                var invailidId = createProject.UsersIds.Except(userHasset).ToList();
                if (invailidId.Any()) {
                    return ApiResponse.Fail("Invailid Ids");
                }
                newProject.OwnerId = userIdString; 
                newProject.Users = getAllUser;
            }
            return ApiResponse.Success(newProject);
        }

        // lấy toàn bộ project của 1 user "đang tham gia"
        [Authorize]
        [HttpGet("project/user")]
        public async Task<ActionResult<ApiResponse>> getProjectByUser() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            var GuidUserId = Guid.Parse(userId);
            var user = await _user.GetAsync(user => user.Id == GuidUserId, includeProperties: "Projects");
            return ApiResponse.Success(user);
        }
        // update project, 
        [Authorize]
        [HttpPatch("{projectId}")]
        public async Task<ActionResult<ApiResponse>> UpdateProjectByOwner(Guid projectId, [FromBody] UpdateProjectDto updateProjectDto) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var GuidUserId = Guid.Parse(userId);
            var checkProject = await _project.GetAsync(project => project.Id == projectId && project.OwnerId == GuidUserId );

            if (checkProject == null) {
                return ApiResponse.Fail("Project not found or not exist");
            }
            else {
                if (updateProjectDto.UsersIds != null)
                    // check xem user co ton tai k
                    foreach (Guid userid in updateProjectDto.UsersIds)
                    {
                        var checkUser = await _user.GetAsync(user => user.Id == userid);
                        if (checkUser == null)
                        {
                            return ApiResponse.Fail($"User not found:{userid}");
                        }
                       

                    }
                checkProject.Users = await _user.GetAllAsync(user => updateProjectDto.UsersIds.Contains(user.Id));

                if (updateProjectDto.ProjectName != null)
                    checkProject.ProjectName = updateProjectDto.ProjectName;
                if (updateProjectDto.StartTime != null)
                    checkProject.StartTime = updateProjectDto.StartTime.Value;

                if (updateProjectDto.EndTime != null) {
                    checkProject.EndTime = updateProjectDto.EndTime.Value;
                }
                   

                if (updateProjectDto.Note != null)
                    checkProject.Note = updateProjectDto.Note;
                await _project.UpdateAsync(checkProject);
                return ApiResponse.Success(checkProject);
            }
            
            
        }
        //xóa section của project đó xong xóa project
        [Authorize]
        [HttpDelete("{projectId}")]
        public async Task<ActionResult<ApiResponse>> DeleteProject(Guid projectId)
        {
            // Lấy project theo Id
            var project = await _project.GetAsync(p => p.Id == projectId);
            if (project == null)
                return ApiResponse.Fail("Project not found");

            // Lấy tất cả section liên quan tới project
            var sections = await _section.GetAllAsync(s => s.ProjectId == projectId);

            // Xóa tất cả todo trong từng section
            foreach (var section in sections)
            {
                var todos = await _todo.GetAllAsync(t => t.TodoSectionId == section.Id);
                foreach (var todo in todos)
                {
                    await _todo.DeleteAsync(todo);
                }
                // Xóa section
                await _section.DeleteAsync(section);
            }

            // Xóa project
            await _project.DeleteAsync(project);

            return ApiResponse.Success("Project and all related sections deleted successfully");
        }

        // lấy mấy cái project của owner
        [Authorize]
        [HttpGet("owner/project")]
        public async Task<ActionResult<ApiListResponse<Projects>>> GetListOwnerProject() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var GuidUserId = Guid.Parse(userId);
            // lấy project từ cái thằng user này
            
            var project = await _project.GetAllAsync(project => project.OwnerId == GuidUserId);
            return ApiListResponse<Projects>.Success(project);
        }
    }
}
