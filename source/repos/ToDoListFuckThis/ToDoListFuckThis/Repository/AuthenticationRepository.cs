using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using BCrypt.Net;
using ToDoListFuckThis.Models.Dto.LoginDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class AuthenticationRepository : IAuthentication
    {
        public readonly ApplicationDbContext _db;
        public readonly IMapper _mapper;
        public AuthenticationRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public bool IsUniqueUser(string email)
        {
            var user = _db.user.FirstOrDefault(u => u.Email == email);
            if (user == null) {
                return false;
            }
            return false;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        // truyền từ register dto vào
        public async Task<Users> RegisterAsync(RegisterRequestDto register)
        {
            var userentity = _mapper.Map<Users>(register);

            userentity.Password = BCrypt.Net.BCrypt.HashPassword(userentity.Password);

            await _db.AddAsync(userentity);
            return userentity;
        }
    }
}
