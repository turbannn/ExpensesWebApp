using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.UserDTOs;

public class UserCreateDTO : BaseDataTransferObject, IUserTransferObject
{
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
    public string Role { get; set; } = null!;

    public string Password { get; set; } = null!;
}