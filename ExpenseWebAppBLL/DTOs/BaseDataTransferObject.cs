 using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs
{
    public abstract class BaseDataTransferObject : ITransferObject
    {
        public int Id { get; set; }
    }
}
