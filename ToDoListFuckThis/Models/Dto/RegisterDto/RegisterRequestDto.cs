using System.ComponentModel.DataAnnotations;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models.Dto.RegisterDto
{
    public class RegisterRequestDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; } //có thể có có thể không
        public int Age { get; set; }
        public string? ImagesUrl { get; set; }

        public string? Email { get; set; }
    }
}
