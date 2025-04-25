using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.UserDTOs
{
    public class UserReadDTO : BaseDataTransferObject, IUserTransferObject
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<ExpenseReadDTO> Expenses { get; set; } = null!;
    }
}
