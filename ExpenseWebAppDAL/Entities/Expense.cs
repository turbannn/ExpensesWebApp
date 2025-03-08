using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public double Value { get; set; }

        [Required(ErrorMessage ="Description must not be empty")]
        public string? Description { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
