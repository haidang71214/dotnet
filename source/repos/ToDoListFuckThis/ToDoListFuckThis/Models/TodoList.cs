using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models
{
    // todolist cho từng cá nhân trong 1 project
    public class Todolists
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        // tên, trạng thái đã làm rồi làm chưa đang làm chưa làm; comment;
        [Required]
        public string Name { get; set; }
        public TaskStatusEnum TaskStatus { get; set; } = TaskStatusEnum.PROGRESS; // trạng thái mặc định là từ từ
        // độ ưu tiên của task,mặc định là không ưu tiên
        public PriorityEnums Priority { get; set; } = PriorityEnums.CLEAR; // mặc định độ quan trong của todo là clear
        // comment
        public string Comment { get; set; }

    }
}
