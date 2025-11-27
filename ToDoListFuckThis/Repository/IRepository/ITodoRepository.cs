using ToDoListFuckThis.Models;

namespace ToDoListFuckThis.Repository.IRepository
{
    // kế thừa lại cái irepository
    public interface ITodoRepository : IRepository<Todolists>
    {
        // get list
        Task<List<Todolists>> GetTodolistsByUserIdAsync(Guid userId);

    }
}
