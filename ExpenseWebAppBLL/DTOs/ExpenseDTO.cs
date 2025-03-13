using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.DTOs
{
    public class ExpenseDTO : IExpenseTransferObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Value must not be empty")] //test
        public double Value { get; set; }

        [Required(ErrorMessage = "Description must not be empty")] //test
        public string Description { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Categories { get; set; }

        public ExpenseDTO()
        {

        }
        public ExpenseDTO(Expense expense)
        {
            Id = expense.Id;
            Value = expense.Value;
            Description = expense.Description;
            CreationDate = expense.CreationDate;

            StringBuilder str = new StringBuilder();
            if(expense.CategoriesList != null)
            {
                foreach (var c in expense.CategoriesList)
                {
                    str.Append(c.Name);
                    str.Append("; ");
                }
            }
            Categories = str.ToString();
        }
    }
}
