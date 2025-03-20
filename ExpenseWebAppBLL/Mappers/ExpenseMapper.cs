using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using System.Text;

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

        internal static Expense ToEntity(IExpenseTransferObject expenseDTO)
        {
            return new Expense
            {
                Id = expenseDTO.Id,
                Value = expenseDTO.Value,
                Description = expenseDTO.Description
            };
        }
    }
}
