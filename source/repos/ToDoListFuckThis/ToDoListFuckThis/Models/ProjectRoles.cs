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
        [ForeignKey("Users")]
        public Guid UsersId { get; set; }
        public Users Users { get; set; } //many to one

        [ForeignKey("Projects")]
        public Guid ProjectsId { get; set; }
        public Projects Projects { get; set;  } // many to one


        public RoleProject roleProject { get; set; } = RoleProject.EMPLOYEE; // mặc định nếu không tạo là nhân viên

    }
}
