using CleanArcitecture.Domain.Entities;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<List<Student>>
    {
    }
}
