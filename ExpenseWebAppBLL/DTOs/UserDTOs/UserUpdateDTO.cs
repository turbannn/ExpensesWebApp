using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.UserDTOs
{
    public class UserUpdateDTO : BaseDataTransferObject, IUserTransferObject
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
    }
}
