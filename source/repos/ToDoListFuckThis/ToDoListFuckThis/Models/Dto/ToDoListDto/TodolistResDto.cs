using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListFuckThis.Enums;

namespace ToDoListFuckThis.Models.Dto.ToDoListDto
{
    public class TodolistResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public TaskStatusEnum? TaskStatus { get; set; } = TaskStatusEnum.PROGRESS; // trạng thái mặc định là từ từ
        // độ ưu tiên của task,mặc định là không ưu tiên
        public PriorityEnums? Priority { get; set; } = PriorityEnums.CLEAR; // mặc định độ quan trong của todo là clear
        // comment
        public string? Comment { get; set; }
        public Users User { get; set; }
    }
}
