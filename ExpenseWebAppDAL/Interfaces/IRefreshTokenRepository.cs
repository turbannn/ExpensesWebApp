using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppDAL.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
    }
}
