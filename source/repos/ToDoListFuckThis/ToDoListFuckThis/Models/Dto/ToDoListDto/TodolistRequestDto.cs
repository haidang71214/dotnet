using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models.Dto.ToDoListDto
{
    public class TodolistRequestDto
    {
        [Required]
        public string Name { get; set; }
        // này mặc định khi tạo mới, khi update thì mình sẽ thay đổi
        public TaskStatusEnum TaskStatus { get; set; } = TaskStatusEnum.PROGRESS; // trạng thái mặc định là từ từ
        // độ ưu tiên của task,mặc định là không ưu tiên
        public PriorityEnums Priority { get; set; } = PriorityEnums.CLEAR; // mặc định độ quan trong của todo là clear
        // comment
        public string Comment { get; set; }
        public string userId { get; set; }
    }
}
