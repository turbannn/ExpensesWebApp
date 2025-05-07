
namespace ExpenseWebAppBLL.Interfaces
{
    public interface IUserTransferObject : ITransferObject
    {
        string Username { get; set; }
        string? Email { get; set; }
        string Role { get; set; }
    }
}
