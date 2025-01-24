using AutoMapper;
using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Students.Queries.DTOs;
using CleanArcitecture.Core.Features.Students.Queries.Models;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler(IStudentService studentService, IMapper mapper) : ResponseHandler,
                IRequestHandler<GetStudentListQuery, Response<List<StudentListDTO>>>,
                IRequestHandler<GetStudentDetailsQuery, Response<StudentDetailsDto>>
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IMapper _mapper = mapper;
        // get student list
        public async Task<Response<List<StudentListDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentsListAsync();
            var result = _mapper.Map<List<StudentListDTO>>(studentList);

            return Success(result, null, new { count = result.Count() });
        }
        // get student by id
        public async Task<Response<StudentDetailsDto>> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
                return NotFound<StudentDetailsDto>();
            var result = _mapper.Map<StudentDetailsDto>(student);
            return Success(result);
        }
    }
}
