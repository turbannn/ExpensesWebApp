using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public double Value { get; set; }
        public string? Description { get; set; }


    }
}
