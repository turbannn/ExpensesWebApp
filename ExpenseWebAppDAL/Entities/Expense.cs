using ExpenseWebAppDAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseWebAppDAL.Entities;

public class Expense : ISoftDeletable
{
    [Column(nameof(Id))]
    public int Id { get; set; }

    [Column(nameof(Value))]
    public double Value { get; set; }

    [Column(nameof(Description))]
    public string Description { get; set; } = null!;

    [Column(nameof(CreationDate))]
    public DateTime? CreationDate { get; set; }

    [Column(nameof(IsDeleted))]
    public bool IsDeleted { get; set; }
    [Column(nameof(DeletedAt))]
    public DateTimeOffset? DeletedAt { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public List<Category>? CategoriesList { get; set; }

    public Expense()
    {
        
    }
}