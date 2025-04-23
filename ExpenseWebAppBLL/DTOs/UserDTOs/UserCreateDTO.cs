using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.UserDTOs;

public class UserCreateDTO : IUserTransferObject
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; }
}