using System.Linq.Expressions;
using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebAppDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAppContext _context;

        public UserRepository(WebAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task AddAsync(User entityToAdd)
        {
            await _context.Users.AddAsync(entityToAdd);
            await _context.SaveChangesAsync();
        }

        // Throws exception if not found or if more than one. Try-catch block is required on higher levels.
        public async Task UpdateAsync(User entityToUpdate)
        {
            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == entityToUpdate.Id);

            if (existingUser == null)
                throw new NullReferenceException();

            existingUser.Username = entityToUpdate.Username;
            existingUser.Password = entityToUpdate.Password;
            existingUser.Role = entityToUpdate.Role;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
        }
    }
}
