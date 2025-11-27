using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models.Dto;
using ToDoListFuckThis.Models.Dto.LoginDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;
using ToDoListFuckThis.Models.Dto.ResetPassDto;
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
        private readonly IEmailService _emailService;
        public AuthController(IMapper mapper, IAuthentication authen, IUserRepository userRepo, IEmailService emailService)
        {
            _mapper = mapper;
            _authen = authen;
            _userRepo = userRepo;
            _emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> LoginUser([FromBody] LoginRequestDto loginRequest) {
            var shit = await _authen.LoginAsync(loginRequest);
            if (shit == null) {
                return ApiResponse.Fail("User not foud");
            }
            return ApiResponse.Success(shit);
        }

        [HttpPost("register")] // sửa lại cái update user
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDto register) {
            var checkUser =  await _userRepo.GetAsync(u => u.Email == register.Email);
            if (checkUser != null) {
                return ApiResponse.Fail("User đã tồn tại");
            }
            var hehe = await _authen.RegisterAsync(register);
            var userDto = _mapper.Map<UserDto>(hehe);
            return ApiResponse.Success(userDto);
        }
        [HttpPost("resetmail")]
        public async Task<ActionResult<ApiResponse>> SendMailReset([FromBody] EmailResetDto email)
        {
            var checkUser = await _userRepo.GetAsync(u => u.Email == email.Email);
            if (checkUser == null)
            {
                return ApiResponse.Fail("User không tồn tại");
            }

            // Tạo token
            var token = Guid.NewGuid().ToString();

            // Lưu token vào DB cùng thời gian hết hạn
            checkUser.ResetToken = token;
            checkUser.TokenExpireTime = DateTime.UtcNow.AddMinutes(15);
            await _userRepo.UpdateAsync(checkUser);

            // Gửi mail
            await _emailService.SendMailAsync(
                email.Email,
                "Reset Password Token",
                $"Token reset của bạn là: {token}",
                $"<p>Token reset của bạn là: <b>{token}</b></p>"
            );

            return ApiResponse.Success("Token đã được gửi về email");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse>> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userRepo.GetAsync(u => u.ResetToken == dto.Token);
            if (user == null)
                return ApiResponse.Fail("Token không hợp lệ");

            if (user.TokenExpireTime < DateTime.UtcNow)
                return ApiResponse.Fail("Token đã hết hạn");

            // Update mật khẩu (hash trước khi lưu)
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // Xoá token và thời hạn
            user.ResetToken = null;
            user.TokenExpireTime = null;

            await _userRepo.UpdateAsync(user);

            return ApiResponse.Success("Đổi mật khẩu thành công");
        }

    }
}
