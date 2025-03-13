using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense, IExpenseTransferObject>
    {
        Task UpdateWithCategoryAsync(IExpenseTransferObject entity);
        Task UpdateAndDeleteCategoryAsync(IExpenseTransferObject entity);
    }
}
