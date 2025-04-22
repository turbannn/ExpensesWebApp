using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities
{
    public class RefreshToken
    {
        public string Token { get; } = Guid.NewGuid().ToString();
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
