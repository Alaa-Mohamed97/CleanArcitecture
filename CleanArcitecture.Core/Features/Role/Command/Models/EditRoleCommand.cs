using CleanArcitecture.Core.Base;
using CleanArcitecture.Domain.Helpers;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Command.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {
    }
}
