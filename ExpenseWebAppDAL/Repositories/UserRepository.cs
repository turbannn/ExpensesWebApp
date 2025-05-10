using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExpenseWebAppDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAppContext _context;
        private IDbContextTransaction? _transaction;

        private readonly Func<WebAppContext, int, int> _сompiledCountQuery =
            EF.CompileQuery((WebAppContext context, int userId) =>
                context.Expenses.Count(e => e.UserId == userId));

        public UserRepository(WebAppContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.CategoriesList)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.CategoriesList)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.CategoriesList)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.CategoriesList)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.CategoriesList)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByIdWithPagedExpensesAsync(int userId, int pageNumber, int pageSize)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;
            
            var expenses = await _context.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .Include(e => e.CategoriesList)
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            user.Expenses = expenses;
            var count = _сompiledCountQuery(_context, userId);

            user.EntityMetadata = new EntityMetadata { UserExpensesTotalCount = count};

            return user;
        }

        public async Task AddAsync(User entityToAdd)
        {
            await _context.Users.AddAsync(entityToAdd);
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
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            _context.Remove(user);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
