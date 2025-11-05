using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models.Dto.RegisterDto
{
    public class RegisterResponseDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public string? ImagesUrl { get; set; }

        public string? Email { get; set; }
        public RoleEnums Role { get; set; } 
    }
}
