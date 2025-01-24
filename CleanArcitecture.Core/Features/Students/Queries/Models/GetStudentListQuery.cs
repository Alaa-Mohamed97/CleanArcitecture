using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Students.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<StudentListDTO>>>
    {
    }
}
