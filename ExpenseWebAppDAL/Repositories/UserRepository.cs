using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebAppDAL.Repositories
{
    public class UserRepository(WebAppContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users
                .AsNoTracking()
                .Include(u => u.RefreshToken)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users
                .AsNoTracking()
                .Include(u => u.RefreshToken)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task AddAsync(User entityToAdd)
        {
            await context.Users.AddAsync(entityToAdd);
            await context.SaveChangesAsync();
        }

        // Throws exception if not found or if more than one. Try-catch block is required on higher levels.
        public async Task UpdateAsync(User entityToUpdate)
        {
            var existingUser = await context.Users
                .Include(u => u.RefreshToken)
                .SingleOrDefaultAsync(u => u.Id == entityToUpdate.Id);

            if (existingUser == null)
                throw new NullReferenceException();

            existingUser.Username = entityToUpdate.Username;
            existingUser.Password = entityToUpdate.Password;
            existingUser.Role = entityToUpdate.Role;
            
            existingUser.RefreshToken = entityToUpdate.RefreshToken;

            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
        }
    }
}
