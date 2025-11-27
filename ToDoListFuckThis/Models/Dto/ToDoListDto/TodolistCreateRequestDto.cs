// TodolistCreateRequestDto.cs
using System.ComponentModel.DataAnnotations;
using ToDoListFuckThis.Enums;

public class TodolistCreateRequestDto
{
    [Required]
    public string? Name { get; set; }

    public TaskStatusEnum? TaskStatus { get; set; } = TaskStatusEnum.PROGRESS;
    public PriorityEnums? Priority { get; set; } = PriorityEnums.CLEAR;
    public string? Comment { get; set; }

    public string? timeStart { get; set; }   // ← ĐỔI THÀNH string?
    public string? timeEnd { get; set; }     // ← ĐỔI THÀNH string?
}