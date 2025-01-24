using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.ApplicationUser.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
