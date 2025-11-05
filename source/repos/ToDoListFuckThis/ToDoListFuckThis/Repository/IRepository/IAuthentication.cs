using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Models.Dto.LoginDto;
using ToDoListFuckThis.Models.Dto.RegisterDto;

namespace ToDoListFuckThis.Repository.IRepository
{
    public interface IAuthentication
    // khai báo hàm, khi làm thì sẽgoij cái này ra, đéo kế thừa, vì nó đéo cần
    {
        bool IsUniqueUser(string email);
        // ở đây, logic register rất dễ nhưng, cần phải chắc chắn đó đã được register
        Task<Users> RegisterAsync(RegisterRequestDto Register);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
