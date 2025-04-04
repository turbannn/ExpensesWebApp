using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.Interfaces
{
    public interface IExpenseTransferObject : ITransferObject
    {
        double Value { get; set; }
        string Description { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
