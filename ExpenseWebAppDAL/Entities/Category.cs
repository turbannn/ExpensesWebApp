using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Must have a name")]
        public string Name { get; set; } = null!;
        
        [NotMapped]
        public int[]? ExpensesIds {  get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}
