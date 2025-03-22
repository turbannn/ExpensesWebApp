using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities;

public class Category : ISoftDeletable
{
    [Column(nameof(Id))]
    public int Id { get; set; }

    [Column(nameof(Name))]
    public string Name { get; set; } = null!;

    public List<Expense>? Expenses { get; set; }

    public Category(){}
    public override string ToString() { return Name; }

    [Column(nameof(IsDeleted))]
    public bool IsDeleted { get; set; }

    [Column(nameof(DeletedAt))]
    public DateTimeOffset? DeletedAt { get; set; }
}
