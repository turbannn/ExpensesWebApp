using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces;

public interface IExpenseRepository : IRepository<Expense>, ITransactionalRepository
{
    Task UpdateAndAddCategoryAsync(Expense entity, int categoryId);
    Task UpdateAndDeleteCategoryAsync(Expense entity, string categoryName);
    Task AddWithCategoryAsync(Expense entity, int categoryId);
    Task<IEnumerable<Expense>> GetAllDeletedByUserIdAsync(int userId);
    Task HardDeleteAsync(int id);
    Task RestoreAsync(int id);
}
