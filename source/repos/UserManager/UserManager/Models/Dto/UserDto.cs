using System.ComponentModel.DataAnnotations;

namespace UserManager.Models.Dto
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ImagesUrl { get; set; }
    }
}
