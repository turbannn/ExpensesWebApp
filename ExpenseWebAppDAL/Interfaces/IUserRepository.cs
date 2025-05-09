using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IUserRepository : IRepository<User>, ITransactionalRepository
    {
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}
