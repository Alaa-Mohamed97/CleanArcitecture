using CleanArcitecture.Core.Features.Students.Command.Models;
using CleanArcitecture.Service.Abstracts;
using FluentValidation;

namespace CleanArcitecture.Core.Features.Students.Command.Validators
{

    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;
        public EditStudentValidator(IStudentService studentService)
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
                .MustAsync(async (model, key, cancellationToken) => !await _studentService.IsNameExistExcludeSelf(key, model.Id))
                .WithMessage("name is exist");
        }
    }
}
