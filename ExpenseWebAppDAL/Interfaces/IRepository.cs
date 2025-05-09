
namespace ExpenseWebAppDAL.Interfaces;

public interface IRepository<TObjectType>
{
    Task<IEnumerable<TObjectType>> GetAllAsync();
    Task<TObjectType?> GetByIdAsync(int id);
    Task AddAsync(TObjectType entityToAdd);
    Task UpdateAsync(TObjectType entityToUpdate);
    Task DeleteAsync(int id);
}
