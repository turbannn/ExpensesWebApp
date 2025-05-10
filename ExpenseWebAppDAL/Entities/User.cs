
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseWebAppDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Email { get; set; }

        [NotMapped] 
        public EntityMetadata EntityMetadata { get; set; } = null!;

        public List<Expense> Expenses { get; set; } = null!;
    }
}
