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
        Task AddAsync(IExpenseTransferObject entityToAdd);
        Task UpdateAsync(IExpenseTransferObject entityToUpdate); //IExpenseTransferObject
        Task DeleteAsync(int id);
    }
}
