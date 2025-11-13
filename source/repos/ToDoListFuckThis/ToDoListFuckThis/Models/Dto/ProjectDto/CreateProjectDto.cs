using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ToDoListFuckThis.Models.Dto.ProjectDto
{
    public class CreateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }

        public string? Note { get; set; } // lưu ý khi làm project
        public List<Guid> UsersIds { get; set; } = new();
        // mỗi project thì sẽ có nhiều users, và có nhiều cái todosection
 // todosection sẽ được tạo theo project 

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // mỗi cái todolist cho project thì có mỗi giai đoạn
    }
}
