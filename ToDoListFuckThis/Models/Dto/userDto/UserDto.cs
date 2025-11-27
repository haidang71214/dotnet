using System.ComponentModel.DataAnnotations;
using ToDoListFuckThis.Enums;

namespace UserManager.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ImagesUrl { get; set; }
        public int Age { get; set; }

        public string? Role { get; set; }
    }
}
