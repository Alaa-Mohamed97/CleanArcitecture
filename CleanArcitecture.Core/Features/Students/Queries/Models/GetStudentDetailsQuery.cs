using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Students.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Queries.Models
{
    public class GetStudentDetailsQuery : IRequest<Response<StudentDetailsDto>>
    {
        public int Id { get; set; }
        public GetStudentDetailsQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
