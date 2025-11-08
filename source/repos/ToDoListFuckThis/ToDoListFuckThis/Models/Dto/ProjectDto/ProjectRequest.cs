namespace ToDoListFuckThis.Models.Dto
{
    public class ProjectRequest
    {
        public string? ProjectName { get; set; }

        public string? Note { get; set; } // lưu ý khi làm project
        public List<int>? UserIds { get; set; } = new();
        // tạo 1 list todo section cho mỗi project xong nhét vô đây
        public List<int>? TodosectionIds { get; set; } = new();

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
