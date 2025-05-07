
namespace ExpenseWebAppDAL.Interfaces
{
    public interface IEmailHandler
    {
        Task SendEmail(string email, string subject, string body);
    }
}
