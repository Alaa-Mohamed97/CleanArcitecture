using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Command.Models
{
    public class DeleteRoleCommand(int Id) : IRequest<Response<string>>
    {
        public int Id { get; set; } = Id;
    }
}
