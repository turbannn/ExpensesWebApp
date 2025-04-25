using ExpenseWebAppBLL.Interfaces;
using FluentValidation;

namespace ExpenseWebAppBLL.Validators
{
    public class UserDTOValidator : AbstractValidator<IUserTransferObject>
    {
        public UserDTOValidator()
        {
            //int
            RuleFor(e => e.Id)
                .GreaterThan(-1).WithMessage("Id is less than 0");

            //str
            RuleFor(e => e.Username)
                .NotEmpty().WithMessage("User Username is empty")
                .MaximumLength(40).WithMessage("User Username Maximum length exceeded");

            RuleFor(e => e.Role)
                .NotEmpty().WithMessage("User Role is empty")
                .MaximumLength(10).WithMessage("User Role Maximum length exceeded");

        }
    }
}
