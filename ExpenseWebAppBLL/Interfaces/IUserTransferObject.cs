
namespace ExpenseWebAppBLL.Interfaces
{
    public interface IUserTransferObject : ITransferObject
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}
