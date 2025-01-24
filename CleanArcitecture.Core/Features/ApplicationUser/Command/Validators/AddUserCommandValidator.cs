using CleanArcitecture.Core.Features.ApplicationUser.Command.Models;
using FluentValidation;

namespace CleanArcitecture.Core.Features.ApplicationUser.Command.Validators
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(u => u.FullName).NotEmpty().NotNull().WithMessage("{PropertyName}:Is required");
            RuleFor(u => u.Email).NotEmpty().NotNull().WithMessage("{PropertyName}:Is required");
            RuleFor(u => u.UserName).NotEmpty().NotNull().WithMessage("{PropertyName}:Is required");
            RuleFor(u => u.Password).NotEmpty().NotNull()
                .WithMessage("{PropertyName}:Is required");
            RuleFor(u => u.ConfirmPassword).NotEmpty().NotNull()
                .WithMessage("{PropertyName}:Is required")
                .Equal(u => u.Password)
                .WithMessage("confirm password must equal password");
        }
    }
}
