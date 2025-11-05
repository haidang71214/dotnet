using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public string? ImagesUrl { get; set; }

        public string? Email { get; set; }
        public RoleEnums Role { get; set; } = RoleEnums.CLIENT; //mặc định là client
    }
}
