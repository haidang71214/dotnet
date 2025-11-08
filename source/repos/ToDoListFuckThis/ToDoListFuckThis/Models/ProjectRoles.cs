using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models
{
    // todolist cho từng cá nhân trong 1 project
    public class ProjectRoles
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();
        // tên, trạng thái đã làm rồi làm chưa đang làm chưa làm; comment;
        public Users? user { get; set; } //many to one
        public Projects? projects { get; set;  } // many to one


        public RoleProject roleProject { get; set; } = RoleProject.EMPLOYEE; // mặc định nếu không tạo là nhân viên

    }
}
