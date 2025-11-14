using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models
{
    public class Users
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public string? ImagesUrl { get; set; }

        [Required]
        public string Email { get; set; }
        public RoleEnums Role { get; set; } = RoleEnums.CLIENT; //mặc định là client
        [InverseProperty("Owner")]
        public ICollection<Projects> OwnedProjects { get; set; } = new List<Projects>();
        public ICollection<Projects> Projects { get; set; } = new List<Projects>();
    }
}
