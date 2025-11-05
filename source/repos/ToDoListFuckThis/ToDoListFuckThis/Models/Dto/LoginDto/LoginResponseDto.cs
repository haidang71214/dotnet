using UserManager.Models.Dto;

namespace ToDoListFuckThis.Models.Dto.LoginDto
{
    public class LoginResponseDto
    {
        // nhớ ép kiểu thành userDto
        public UserDto user { get; set; }
        public string Token { get; set; }

    }
}
