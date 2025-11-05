using System.ComponentModel.DataAnnotations;

namespace UserManager.Models.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        public string? ImagesUrl { get; set; }
        public int Age { get; set; }
    }
}
