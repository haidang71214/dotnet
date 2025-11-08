using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models.CustomResponse;
using ToDoListFuckThis.Models.Dto.LoginDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.Models.Dto;
using UserManager.repository.IRepository;

namespace ToDoListFuckThis.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authen;
        private readonly IUserRepository _userRepo;
        public AuthController(IMapper mapper, IAuthentication authen, IUserRepository userRepo) {
            _mapper = mapper;
            _authen = authen;
            _userRepo = userRepo;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> LoginUser([FromBody] LoginRequestDto loginRequest) {
            var shit = await _authen.LoginAsync(loginRequest);
            return shit;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDto register) {
            var checkUser =  await _userRepo.GetAsync(u => u.Email == register.Email);
            if (checkUser != null) {
                return ApiResponse.Fail("User đã tồn tại");
            }
            var hehe = await _authen.RegisterAsync(register);
            var userDto = _mapper.Map<UserDto>(hehe);
            return ApiResponse.Success(userDto);
        }

    }
}
