using System.ComponentModel.DataAnnotations;

namespace ToDoListFuckThis.Models.Dto.TodoSectionDto
{
    public class CreateSection
    {
        // để phần section nắm todolist, phân ra từng deadline
        // add cái list guid todolist vào đây
        //public ICollection<Todolists> Todolists { get; set; }
        // project id lấy ở params
        public List<Guid> Todolists { get; set; } = new();
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
