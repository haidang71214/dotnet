using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse;
using ToDoListFuckThis.Models.Dto.ToDoListDto;
using ToDoListFuckThis.Models.Dto.TodoSectionDto;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.repository.IRepository;
using static System.Collections.Specialized.BitVector32;

namespace ToDoListFuckThis.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IProjectRepository _project;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _section;
        private readonly ITodoRepository _todo;
        public SectionController(IProjectRepository project, IMapper mapper, IUserRepository user,ISectionRepository section,ITodoRepository todo) { 
            _project = project;
            _mapper = mapper;
            _user = user;
            _section = section;
            _todo = todo;
        }
        // xem list project của chính bản thân mình

        // crud cái section, có cả project id và todolist id
        [HttpPost("{projectId}")]
        public async Task<ActionResult<ApiResponse>> createSectionByProjectId(Guid projectId, [FromBody] CreateSection createSection ) {
            var isdefine = await _project.GetAsync(project => project.Id == projectId);
            if (isdefine == null) {
                ApiResponse.Fail("Not Exits");
            }
            // kiểm tra xem những todo đó có tồn tại không

            var mapSection =  _mapper.Map<TodoSection>(createSection);
            mapSection.ProjectId = projectId;

            // 3. Kiểm tra tất cả Todolist IDs có tồn tại không
            if (createSection.Todolists?.Any() == true)
            {
                var validTodoIds = await _todo.GetAllAsync(t => createSection.Todolists.Contains(t.Id));
                var validTodoIdSet = validTodoIds.Select(t => t.Id).ToHashSet();

                var invalidIds = createSection.Todolists.Except(validTodoIdSet).ToList();
                if (invalidIds.Any())
                    return ApiResponse.Fail($"Invalid TodoList IDs: {string.Join(", ", invalidIds)}");

                // Gán navigation property (nếu cần EF theo dõi quan hệ)
                mapSection.Todolists = validTodoIds;
            }
            var createSectiona= await _section.CreateAsync(mapSection);
            if (createSectiona == null)
                return ApiResponse.Fail("Failed to create section");
            return ApiResponse.Success(createSection);
        }
        // lấy tất cả section của project
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ApiListResponse<TodoSection>>> getSectionByProject(Guid projectId) { 
            var GetFuckProject =  await _section.GetAsync(project => project.Id == projectId);
            if (GetFuckProject == null) {
                ApiResponse.Fail("Project not exits");
            }
            var section = await _section.GetAllAsync(t => t.ProjectId == projectId);
            return ApiListResponse<TodoSection>.Success(section);
        }
        // update section by section Id
        [HttpPatch("{sectionId}")]
        public async Task<ActionResult<ApiResponse>> UpdateSectionById(Guid sectionId,UpdateSectionDto update) {
            // lấy chính cái section id đó ra update
            // 
            var checkSection = await _section.GetAsync(section => section.Id == sectionId);
            if (checkSection == null) {
                return ApiResponse.Fail("Section not exits");
            }
            if (update.Todolists != null) {
                // kiếm cái todo rồi add vào
               var vailidTodo = await _todo.GetAllAsync(t => update.Todolists.Contains(t.Id)); // lấy ids của todo 
                var validTodoIdSet = vailidTodo.Select(t => t.Id).ToHashSet();
                if (validTodoIdSet.Any())
                    return ApiResponse.Fail("Invailid Ids");

                checkSection.Todolists = vailidTodo;
            }
            if (update.DateEnd != null) {
                checkSection.DateEnd = update.DateEnd;
            }
            if (update.DateStart != null)
            {   
                checkSection.DateStart = update.DateStart;
            }
           checkSection.ProjectId = checkSection.ProjectId;
            checkSection.Id = checkSection.Id;
            await _section.UpdateAsync(checkSection);
            return ApiResponse.Success(checkSection);           
        }
        // xóa section (xóa cả todolist lẫn trong project)
        [HttpDelete("{sectionId}")]
        public async Task<ActionResult<ApiResponse>> DeleteSectionById(Guid sectionId) { 

            var getSection = await _section.GetAsync(t => t.Id == sectionId);
            if (getSection == null) {
                return ApiResponse.Fail("a"); 
          }

            var getListTodoList = await _todo.GetAllAsync(t=> t.TodoSectionId == sectionId);
            foreach (var todo in getListTodoList) {
                await _todo.DeleteAsync(todo);
            }
            await _section.DeleteAsync(getSection);

            return ApiResponse.Success("delete Success");
        }
        
    }
}
