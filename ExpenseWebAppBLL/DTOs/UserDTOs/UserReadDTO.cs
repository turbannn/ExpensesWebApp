using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.UserDTOs
{
    public class UserReadDTO : BaseDataTransferObject, IUserTransferObject
    {
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;

        public List<ExpenseReadDTO> Expenses { get; set; } = null!;
    }
}
