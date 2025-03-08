using ExpenseWebAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>?> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category entityToAdd);
        Task UpdateAsync(Category entityToUpdate);
        Task DeleteAsync(int id);
    }
}
