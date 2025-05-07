
namespace ExpenseWebAppDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Email { get; set; }

        public List<Expense> Expenses { get; set; } = null!;
    }
}
