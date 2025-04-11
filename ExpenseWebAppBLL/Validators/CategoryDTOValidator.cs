using ExpenseWebAppBLL.Interfaces;
using FluentValidation;

namespace ExpenseWebAppBLL.Validators
{
    public class CategoryDTOValidator : AbstractValidator<ICategoryTransferObject>
    {
        public CategoryDTOValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(-1).WithMessage("Id is less than 0");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category Name empty error")
                .Must(BeMaximumOf35Symbols).WithMessage("Category Name more than 35 symbols error");
        }

        private bool BeMaximumOf35Symbols(string name)
        {
            return name.Length < 36;
        }
    }
}
