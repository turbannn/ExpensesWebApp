using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities;

public class Category
{
    [Column(nameof(Id))]
    public int Id { get; set; }

    [Column(nameof(Name))]
    public string Name { get; set; } = null!;
    
    [NotMapped]
    public int[]? ExpensesIds {  get; set; }
    public List<Expense>? Expenses { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
