using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse;
using ToDoListFuckThis.Models.Dto.ToDoListDto;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.repository.IRepository;

namespace ToDoListFuckThis.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoRepository _db;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _section;
        public TodoListController(ITodoRepository db, IMapper mapper, IUserRepository userDb, ISectionRepository sectionRepository)
        {
            _db = db;
            _mapper = mapper;
            _user = userDb;
            _section = sectionRepository;
        }
        [HttpGet("getalltodolist")]
        public async Task<ActionResult<ApiListResponse<Todolists>>> GetAllTodoList()
        {
            var todo = await _db.GetAllAsync();
            return ApiListResponse<Todolists>.Success(todo);
        }
        // tự tạo 
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> CreateTodo([FromBody] TodolistCreateRequestDto todolistRequestDto)
        {
            // tự nhét cái user id vào todo
            var TodoChange = _mapper.Map<Todolists>(todolistRequestDto);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          
            var fuckguid = Guid.Parse(userId);
            TodoChange.UsersId = fuckguid;
            var Todo = await _db.CreateAsync(TodoChange);
            return ApiResponse.Success(Todo);
        }
        // cái người chủ project tạo
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiListResponse<Todolists>>> GetTodoMySelf() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var changeGuiId = Guid.Parse(userId);
            var TodoListList = await _db.GetTodolistsByUserIdAsync(changeGuiId);
            return ApiListResponse<Todolists>.Success(TodoListList);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateTodo(string id, [FromBody] TodolistUpdateRequestDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuiId = Guid.Parse(userId);
            var todoId = Guid.Parse(id);

            // Lấy entity hiện tại từ DB
            var todo = await _db.GetAsync(t => t.Id == todoId);
            if (todo == null)
                return ApiResponse.Fail("Todo not found or unauthorized");

            // Gán từng field nếu DTO có giá trị
            if (dto.Name != null) todo.Name = dto.Name;
            if (dto.Comment != null) todo.Comment = dto.Comment;
            if (dto.Priority != null) todo.Priority = dto.Priority.Value;
            if (dto.TaskStatus != null) todo.TaskStatus = dto.TaskStatus.Value;

            // User không update từ PATCH, giữ nguyên
            todo.User = await _user.GetAsync(u => u.Id == userGuiId);

            // Save entity
            await _db.UpdateAsync(todo);

            return ApiResponse.Success(todo);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetDetailTodoList(string id){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             var userGuiId = Guid.Parse(userId);
            var todoId = Guid.Parse(id);
            var todoList = await _db.GetAsync(todo => todo.Id == todoId,includeProperties:"User");
            return ApiResponse.Success(todoList);
    }
        // trước hết là những cái todo list của mình, mình quản lí như vậy đã
        // có 1 cái để thằng quản lí dự án tạo todolist cho cái thằng tham gia dự án
        [Authorize]
        [HttpPost("section/{id}/users/{userId}")] // có thể là lấy cái userId từ cái api luôn
        public async Task<ActionResult<ApiResponse>> CreateTodoListByProject(string id,string userId, [FromBody] CreateTodoListSectionDto createTodoListSectionDto) {
            var sectionId = Guid.Parse(id); // lấy cái sectionid đó, 
            var userIda = Guid.Parse(userId); // lấy cái userId
            var todoEntity = _mapper.Map<Todolists>(createTodoListSectionDto);
            todoEntity.UsersId = userIda;
            // lấy cái id của section id
            todoEntity.TodoSectionId = sectionId;

            var newTodo = await _db.CreateAsync(todoEntity);   
            return ApiResponse.Success(newTodo);
        }
        // sẽ có 1 cái cho thằng admin của project đó update nữa

        [Authorize]
        [HttpGet("section/{id}")] // có thể là lấy cái userId từ cái api luôn
        public async Task<ActionResult<ApiListResponse<Todolists>>> GetTodoListBySection(string id)
        {
            var sectionId = Guid.Parse(id); // lấy cái sectionid đó, 

            var listtodo = await _db.GetAllAsync(u => u.TodoSectionId == sectionId);
   
            return ApiListResponse<Todolists>.Success(listtodo);
        }
        [Authorize]
        [HttpDelete("{todoId}")]
        public async Task<ActionResult<ApiResponse>> DeleteTodo(Guid todoId) {
            var todo = await _db.GetAsync(todo => todo.Id == todoId);
            await _db.DeleteAsync(todo);
            return ApiResponse.Success(todo);
        }
    }
}
