using ContactManager.API.Dtos.Requests;
using FluentValidation;

namespace ContactManager.Application.Validators
{
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {

        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
