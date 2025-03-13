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
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Value must not be empty")] //test
    public double Value { get; set; }

    [Required(ErrorMessage = "Description must not be empty")] //test
    public string Description { get; set; } = null!;
    public DateTime? CreationDate { get; set; }
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
