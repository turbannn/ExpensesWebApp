using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.ExpenseDTOs
{
    public class ExpenseReadDTO : BaseDataTransferObject, IExpenseTransferObject
    {
        public double Value { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public string? Categories { get; set; }
    }
}
