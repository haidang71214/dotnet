namespace ToDoListFuckThis.Repository.IRepository
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string text, string html = null);
    }

}
