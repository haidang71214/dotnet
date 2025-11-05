using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListFuckThis.Models
{
    // giai đoạn cho từng cái project, phân ra mỗi giai đoạn là 1 cái todolist riêng
    public class TodoSection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        // để phần section nắm todolist, phân ra từng deadline
        public List<Todolists> Todolists { get; set; } = new List<Todolists>();
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }
}
