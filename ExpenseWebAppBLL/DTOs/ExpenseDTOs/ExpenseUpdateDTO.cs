using ExpenseWebAppBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.DTOs.ExpenseDTOs
{
    public class ExpenseUpdateDTO : BaseDataTransferObject, IExpenseTransferObject
    {
        public int UserId { get; set; }
        public double Value { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public string? Categories { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}