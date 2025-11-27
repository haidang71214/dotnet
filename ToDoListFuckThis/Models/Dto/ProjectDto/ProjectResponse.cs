using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;
using UserManager.Models.Dto;

namespace ToDoListFuckThis.Models.Dto
{
    public class ProjectResponse
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }

        public string? Note { get; set; } // lưu ý khi làm project
        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public List<TodoSection> Todosection { get; set; } = new List<TodoSection>();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
