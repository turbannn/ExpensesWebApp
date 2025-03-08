using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task AddAsync(Expense entityToAdd);
        Task UpdateAsync(Expense entityToUpdate);
        Task DeleteAsync(int id);
    }
}
