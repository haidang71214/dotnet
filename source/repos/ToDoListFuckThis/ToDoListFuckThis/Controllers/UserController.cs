using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.CustomResponse; // THÊM DÒNG NÀY
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
        public async Task<ActionResult<ApiResponse>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
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
        public async Task<ActionResult<ApiResponse>> getDetailUser(int id) {
            var user = await _db.GetAsync(u => u.Id == id);
            if (user == null) {
                return ApiResponse.Fail("NotFound"); 
            }
            return ApiResponse.Success(_mapper.Map<UserDto>(user));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> deleteUserAsync(int id) {
            Users concac = await _db.GetAsync(user => user.Id == id);
            if (concac == null) {
                ApiResponse.Fail("NotFound");
            }
             await _db.DeleteAsync(concac);
            return ApiResponse.Success("Delete Success");
        }
    }
}
