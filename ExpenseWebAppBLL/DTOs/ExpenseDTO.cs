using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs
{
    public class ExpenseDTO : BaseDataTransferObject, IExpenseTransferObject
    {
        public double Value { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Categories { get; set; }

        public ExpenseDTO()
        {

        }
    }
}
