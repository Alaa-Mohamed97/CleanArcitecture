using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Command.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
