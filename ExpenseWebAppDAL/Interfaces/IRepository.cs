using ExpenseWebAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Interfaces;

public interface IRepository<ObjectType>
{
    Task<IEnumerable<ObjectType>> GetAllAsync();
    Task<ObjectType?> GetByIdAsync(int id);
    Task AddAsync(ObjectType entityToAdd);
    Task UpdateAsync(ObjectType entityToUpdate);
    Task DeleteAsync(int id);
}
