using AutoMapper;
using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Students.Command.Models;
using CleanArcitecture.Core.SharedResources;
using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArcitecture.Core.Features.Students.Command.Handlers
{
    public class StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<GlobalMessages> stringLocalizer) : ResponseHandler,
        IRequestHandler<AddStudentRequestCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<GlobalMessages> _stringLocalizer = stringLocalizer;

        public async Task<Response<string>> Handle(AddStudentRequestCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            var result = await _studentService.Create(student);
            if (result.Equals("Success"))
                return Success("added");
            else
                return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var isStudentExist = await _studentService.GetStudentByIdAsync(request.Id);
            if (isStudentExist == null) return NotFound<string>(_stringLocalizer.GetString(GlobalMessages.NotFound));
            var student = _mapper.Map<Student>(request);
            var result = await _studentService.Edit(student);
            if (result.Equals("Success"))
                return Success("updated");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<string>("student not found");
            var result = await _studentService.Delete(student);
            if (result.Equals("Success"))
                return Deleted<string>();
            else
                return BadRequest<string>();
        }
    }
}
