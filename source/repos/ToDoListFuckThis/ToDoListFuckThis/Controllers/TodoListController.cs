using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            TodoChange.User = await _user.GetAsync(u => u.Id == fuckguid);
            var Todo = await _db.CreateAsync(TodoChange);
            Todo.User = null;
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
        public async Task<ActionResult<ApiResponse>> UpdateTodo(string id, TodolistUpdateRequestDto todolistUpdateRequestDto) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuiId = Guid.Parse(userId);
            var todoId = Guid.Parse(id);
            var newTodo = _mapper.Map<Todolists>(todolistUpdateRequestDto);
            newTodo.Id = todoId;
            newTodo.User = await _user.GetAsync(user => user.Id == userGuiId);
            // lấy 2 cái đó nhét vô dưới là có đủ trường như java =))
            await _db.UpdateAsync(newTodo);
            return ApiResponse.Success(newTodo);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetDetailTodoList(string id){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             var userGuiId = Guid.Parse(userId);
            var todoId = Guid.Parse(id);
            var todoList = await _db.GetAsync(todo => todo.Id == todoId);
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
            todoEntity.User = await _user.GetAsync(u=>u.Id == userIda);
            // lấy cái id của section id
            todoEntity.TodoSectionId = sectionId;

            var newTodo = await _db.CreateAsync(todoEntity);   
            return ApiResponse.Success(newTodo);
        }
        [Authorize]
        [HttpPatch("section/{id}/users/{userId}")] // có thể là lấy cái userId từ cái api luôn
        public async Task<ActionResult<ApiResponse>> UpdateTodoListByProject(string id, string userId, [FromBody] UpdateTodoListSection createTodoListSectionDto)
        {
            var sectionId = Guid.Parse(id); // lấy cái sectionid đó, 
            var userIda = Guid.Parse(userId); // lấy cái userId
            var todoEntity = _mapper.Map<Todolists>(createTodoListSectionDto);
            todoEntity.User = await _user.GetAsync(u => u.Id == userIda);
            // lấy cái id của section id
            todoEntity.TodoSectionId = sectionId;

             await _db.UpdateAsync(todoEntity);
            return ApiResponse.Success(todoEntity);
        }

        [Authorize]
        [HttpGet("section/{id}")] // có thể là lấy cái userId từ cái api luôn
        public async Task<ActionResult<ApiListResponse<Todolists>>> GetTodoListBySection(string id)
        {
            var sectionId = Guid.Parse(id); // lấy cái sectionid đó, 

            var listtodo = await _db.GetAllAsync(u => u.TodoSectionId == sectionId);
   
            return ApiListResponse<Todolists>.Success(listtodo);
        }
    }
}
