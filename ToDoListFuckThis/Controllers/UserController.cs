using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse; // THÊM DÒNG NÀY
using ToDoListFuckThis.Models.Dto;
using UserManager.Models.Dto;
using UserManager.repository.IRepository;

namespace ToDoListFuckThis.Controllers
{
    // 
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // injection : 
        private readonly IUserRepository _db;
        private readonly IMapper _mapper;
        public UserController(IUserRepository db,IMapper mapper) {
            _db = db;
            _mapper = mapper;
        }
        // controller router  and logic:
        [HttpGet]
        public async Task<ActionResult<ApiListResponse<UserDto>>> GetAllUser()
        {
            var users = await _db.GetAllAsync();
            var dtos = _mapper.Map<List<UserDto>>(users);

            return ApiListResponse<UserDto>.Success(dtos);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateUser(Guid  id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _db.GetAsync(u => u.Id == id);
            if (user == null)
                return ApiResponse.Fail("User Not Found", HttpStatusCode.NotFound);
            var userDto =  _mapper.Map<Users>(updateUserDto);

            await _db.UpdateAsync(userDto); 

            var userDtoa = _mapper.Map<UserDto>(updateUserDto);
            return ApiResponse.Success(userDtoa);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> getDetailUser(Guid id) {
            var user = await _db.GetAsync(u => u.Id == id);
            if (user == null) {
                return ApiResponse.Fail("NotFound"); 
            }
            return ApiResponse.Success(_mapper.Map<UserDto>(user));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> deleteUserAsync(Guid id) {
            Users concac = await _db.GetAsync(user => user.Id == id);
            if (concac == null) {
                ApiResponse.Fail("NotFound");
            }
             await _db.DeleteAsync(concac);
            return ApiResponse.Success("Delete Success");
        }
        // bất cứ cái nào có cái này đều xác thực được, author ở .net khá giống nodejs ở phần truy suất, khá dễ
        [Authorize]
        //[Authorize(Roles = "ADMIN")]
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse>> getUserInformation() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var userIdString = Guid.Parse(userId);
            var userInfomation = await _db.GetAsync(U => U.Id == userIdString);
        
            return ApiResponse.Success(_mapper.Map<UserDto>(userInfomation)); 
        }
        [Authorize]
        [HttpPatch("profile")]
        public async Task<ActionResult<ApiResponse>> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _db.GetAsync(u => u.Id == userId);

            if (user == null)
                return ApiResponse.Fail("User Not Found", HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.ImagesUrl))
                user.ImagesUrl = dto.ImagesUrl;

            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);  // Hash lại

            if (dto.Age.HasValue)
                user.Age = dto.Age.Value;
            // Save
            await _db.UpdateAsync(user);

            var updatedUserDto = _mapper.Map<UserDto>(user);

            return ApiResponse.Success(updatedUserDto);
        }

    }
}
