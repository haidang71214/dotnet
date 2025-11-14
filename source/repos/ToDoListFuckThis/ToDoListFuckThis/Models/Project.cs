    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    namespace ToDoListFuckThis.Models
    {
        public class Projects 
        {
            [Key]

            public Guid Id { get; set; } = Guid.NewGuid();

            [Required]
            public string ProjectName { get; set; }

            public string? Note { get; set; } // lưu ý khi làm project
            [Required]
            public Guid OwnerId { get; set; }

            [ForeignKey(nameof(OwnerId))]
            [InverseProperty("OwnedProjects")]
            public Users Owner { get; set; } // còn cái user thì map ngược lại đây

            public ICollection<Users> Users { get; set; } = new List<Users>();
            // mỗi project thì sẽ có nhiều users, và có nhiều cái todosection
            public ICollection<TodoSection> Todosection { get; set; } = new List<TodoSection>();

            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            // mỗi cái todolist cho project thì có mỗi giai đoạn
        }
    }
