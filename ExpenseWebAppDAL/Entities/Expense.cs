using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public List<Category>? CategoriesList { get; set; }

    public Expense()
    {
        
    }
}