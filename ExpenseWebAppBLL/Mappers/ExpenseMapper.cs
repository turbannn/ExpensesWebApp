using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.Mappers
{
    internal static class ExpenseMapper
    {
        internal static ExpenseDTO ToDTO(Expense expense)
        {
            StringBuilder str = new StringBuilder();
            if (expense.CategoriesList != null)
            {
                foreach (var c in expense.CategoriesList)
                {
                    str.Append(c.Name);
                    str.Append("; ");
                }
            }

            return new ExpenseDTO
            {
                Id = expense.Id,
                Value = expense.Value,
                Description = expense.Description,
                CreationDate = expense.CreationDate,
                Categories = str.ToString()
            };
        }

        //No copy of Id. Mind using tracked update method
        internal static Expense ToEntity(IExpenseTransferObject expenseDTO)
        {
            return new Expense
            {
                Value = expenseDTO.Value,
                Description = expenseDTO.Description
            };
        }
    }
}
