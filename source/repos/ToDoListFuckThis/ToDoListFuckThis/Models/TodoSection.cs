using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListFuckThis.Models
{
    // giai đoạn cho từng cái project, phân ra mỗi giai đoạn là 1 cái todolist riêng
    public class TodoSection
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        // để phần section nắm todolist, phân ra từng deadline
        [ForeignKey(nameof(Projects))]
        public Guid ProjectId { get; set; }
        public Projects Projects { get; set; }
        public ICollection<Todolists> Todolists { get; set; } = new List<Todolists>();
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }
}
