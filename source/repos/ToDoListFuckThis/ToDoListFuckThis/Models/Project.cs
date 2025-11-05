using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ToDoListFuckThis.Models
{
    public class Projects 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        public string ProjectName { get; set; }

        public string? Note { get; set; } // lưu ý khi làm project
        public List<Users> Users { get; set; } = new List<Users>();
        public List<TodoSection> Todosection { get; set; } = new List<TodoSection>();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // mỗi cái todolist cho project thì có mỗi giai đoạn
    }
}
