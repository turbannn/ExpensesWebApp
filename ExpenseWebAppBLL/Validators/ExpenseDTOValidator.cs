using ExpenseWebAppBLL.Interfaces;
using FluentValidation;

namespace ExpenseWebAppBLL.Validators
{
    public class ExpenseDTOValidator : AbstractValidator<IExpenseTransferObject>
    {
        public ExpenseDTOValidator()
        {
            //int
            RuleFor(e => e.Value)
                .NotEmpty().WithMessage("Expense Value empty error")
                .GreaterThan(0).WithMessage("Expense Value less than 0 error");

            //str
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Expense Description empty error");
        }
    }
}
