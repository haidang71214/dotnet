using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.LoginDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Repository
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        public AuthenticationRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _secretKey = configuration["ApiSettings:Secret"]
                         ?? throw new InvalidOperationException("Missing ApiSettings:Secret in configuration");
        }

        public bool IsUniqueUser(string email)
        {
            var user = _db.user.FirstOrDefault(u => u.Email == email);
            return user == null; // true = chưa có → unique
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _db.user.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey); // SỬA: UTF8, KHÔNG ASCII

            var roleString = user.Role.ToString() ?? "CLIENT"; // SỬA: an toàn với null

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, roleString)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "YourApp",     // PHẢI KHỚP Program.cs
                Audience = "YourApp",   // PHẢI KHỚP Program.cs
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponseDto
            {
                Token = tokenString,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<Users?> RegisterAsync(RegisterRequestDto register)
        {
            if (!IsUniqueUser(register.Email))
                return null;

            var userEntity = _mapper.Map<Users>(register);
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

            await _db.user.AddAsync(userEntity);
            await _db.SaveChangesAsync();

            userEntity.Password = null; // không trả mật khẩu
            return userEntity;
        }
    }
}