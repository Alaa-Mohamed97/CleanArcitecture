using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Command.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            Id = id;
        }
    }
}
