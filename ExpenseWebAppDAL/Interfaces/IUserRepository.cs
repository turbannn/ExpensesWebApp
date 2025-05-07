using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User?> GetByUsernameAsync(string username);
        
    }
}
