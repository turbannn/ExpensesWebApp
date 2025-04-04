using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.Interfaces
{
    public interface ICategoryTransferObject : ITransferObject
    {
        string Name { get; set; }
    }
}
