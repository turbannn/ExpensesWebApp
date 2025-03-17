using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities;

public class Expense
{
    [Column("expense_id")]
    public int Id { get; set; }

    [Column("expense_value")]
    public double Value { get; set; }

    [Column("expense_description")]
    public string Description { get; set; } = null!;
    [Column("expense_creationDate")]
    public DateTime? CreationDate { get; set; }
    [Column("expense_categories")]
    public string? Categories {  get; set; }

    [NotMapped]
    public int[]? CategoriesIds { get; set; }
    public List<Category>? CategoriesList { get; set; }

    public Expense()
    {
        
    }
    public Expense(int id, double value, string description)
    {
        Id = id;
        Value = value;
        Description = description;
    }
}
