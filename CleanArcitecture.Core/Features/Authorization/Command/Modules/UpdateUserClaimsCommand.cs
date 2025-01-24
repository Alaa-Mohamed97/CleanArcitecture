using CleanArcitecture.Core.Base;
using CleanArcitecture.Service.Dtos.Authorization;
using MediatR;

namespace CleanArcitecture.Core.Features.Authorization.Command.Modules
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsDto, IRequest<Response<string>>
    {
    }
}
