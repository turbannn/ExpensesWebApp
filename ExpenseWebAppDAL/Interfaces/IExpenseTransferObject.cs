using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IExpenseTransferObject : ITransferObject
    {
        double Value { get; set; }
        string Description { get; set; }
        int CategoryId { get; set; } //used when want to add
        string CategoryName { get; set; } //used when want to delete
    }
}
