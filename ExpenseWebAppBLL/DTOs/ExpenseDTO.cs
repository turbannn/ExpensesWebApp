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
        public string? Description { get; set; }
        public int CategoryId {  get; set; }    

        public string? Categories { get; set; }
    }
}
