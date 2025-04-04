using ExpenseWebAppDAL.Entities;
using System.Text;
using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.Mappers
{
    internal class ExpenseMapper : IMapper<IExpenseTransferObject, Expense>
    {
        public IExpenseTransferObject ToReadDTO(Expense expense)
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

            return new ExpenseReadDTO
            {
                Id = expense.Id,
                Value = expense.Value,
                Description = expense.Description,
                CreationDate = expense.CreationDate,
                Categories = str.ToString()
            };
        }
        public ExpenseUpdateDTO ToUpdateDTO(Expense expense)
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

            return new ExpenseUpdateDTO
            {
                Id = expense.Id,
                Value = expense.Value,
                Description = expense.Description,
                CreationDate = expense.CreationDate,
                Categories = str.ToString()
            };
        }
        public Expense ToEntity(IExpenseTransferObject dto)
        {
            return new Expense
            {
                Id = dto.Id,
                Value = dto.Value,
                Description = dto.Description,
                CreationDate = dto.CreationDate
            };
        }
    }
}
