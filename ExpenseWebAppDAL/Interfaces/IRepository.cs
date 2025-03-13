using ExpenseWebAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IRepository<ObjectType, TransferObjectType>
    {
        Task<IEnumerable<ObjectType>> GetAllAsync();
        Task<ObjectType?> GetByIdAsync(int id);
        Task AddAsync(TransferObjectType entityToAdd);
        Task UpdateAsync(TransferObjectType entityToUpdate); //IExpenseTransferObject
        Task DeleteAsync(int id);
    }
}
