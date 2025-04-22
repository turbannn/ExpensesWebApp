using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebAppDAL.Repositories
{
    public class RefreshTokenRepository(WebAppContext context) : IRefreshTokenRepository
    {
        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            return await context.RefreshTokens
                .AsNoTracking()
                .Include(rt => rt.User)
                .ToListAsync();
        }

        public async Task<RefreshToken?> GetByIdAsync(int id)
        {
            return await context.RefreshTokens
                .AsNoTracking()
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task AddAsync(RefreshToken entityToAdd)
        {
            await context.RefreshTokens.AddAsync(entityToAdd);
            await context.SaveChangesAsync();
        }
        
        // Throws exception if not found or if more than one. Try-catch block is required on higher levels.
        public async Task UpdateAsync(RefreshToken entityToUpdate)
        {
            var existingToken = await context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Id == entityToUpdate.Id);

            if (existingToken == null)
                throw new NullReferenceException();

            existingToken.Expires = entityToUpdate.Expires;
            existingToken.IsRevoked = entityToUpdate.IsRevoked;
            existingToken.UserId = entityToUpdate.UserId;

            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await context.RefreshTokens.Where(u => u.Id == id).ExecuteDeleteAsync();
        }
    }
}
