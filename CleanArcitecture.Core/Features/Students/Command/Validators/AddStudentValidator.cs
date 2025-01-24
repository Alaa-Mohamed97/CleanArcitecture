using CleanArcitecture.Core.Features.Students.Command.Models;
using CleanArcitecture.Service.Abstracts;
using FluentValidation;

namespace CleanArcitecture.Core.Features.Students.Command.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentRequestCommand>
    {
        private readonly IStudentService _studentService;

        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(x => x.NameEn).NotEmpty()
                .WithMessage("name cannot be empty")
                .NotNull()
                .WithMessage("name can not be null");
            RuleFor(x => x.Phone).NotEmpty()
                .NotNull()
                .WithMessage("phone is required");
            RuleFor(s => s.NameEn)
                .MustAsync(async (key, cancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage("name is exist");
        }
    }
}
