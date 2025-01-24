using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Authorization.Command.Modules
{
    public class UpdateUserRolesCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public List<string> RoleName { get; set; } = null!;
    }

}
