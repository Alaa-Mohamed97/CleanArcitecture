using CleanArcitecture.Core.Features.Students.Queries.Models;
using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler : IRequestHandler<GetStudentListQuery, List<Student>>
    {
        private readonly IStudentService _studentService;

        public StudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentsListAsync();
        }
    }
}
