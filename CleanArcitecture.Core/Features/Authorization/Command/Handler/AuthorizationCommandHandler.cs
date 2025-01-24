using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Authorization.Command.Modules;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Authorization.Command.Handler
{
    internal class AuthorizationCommandHandler(IAuthorizeService authorizeService) : ResponseHandler,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IAuthorizeService _authorizeService = authorizeService;


        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.UpdateUserRoles(request.UserId, request.RoleName);
            return result switch
            {
                "UserNotFound" => NotFound<string>("User Not Found"),
                "CannotRemoveOldRoles" => BadRequest<string>("An Error occured when remove old roles"),
                "Failed" => BadRequest<string>(" User role Not Update"),
                _ => Success(result)
            };
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.UpdateUserClaims(request);
            return result switch
            {
                "UserNotFound" => NotFound<string>("User Not Found"),
                "CannotRemoveOldClaims" => BadRequest<string>("An Error occured when remove old claims"),
                "Failed" => BadRequest<string>("User claims Not Update"),
                _ => Success(result)
            };
        }
    }
}
