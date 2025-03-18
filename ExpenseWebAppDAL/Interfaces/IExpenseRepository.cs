using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces;

public interface IExpenseRepository : IRepository<Expense>
{
    Task UpdateWithCategoryAsync(Expense entity, int categoryId);
    Task UpdateAndDeleteCategoryAsync(Expense entity, string categoryName);
    Task AddWithCategoryAsync(Expense entity, int categoryId);
}
