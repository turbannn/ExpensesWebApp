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

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("User Password empty error")
                .MaximumLength(50).WithMessage("User Password Maximum length exceeded");

        }
    }
}
