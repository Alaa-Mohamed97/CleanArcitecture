using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Authorization.Queries.Models;
using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Service.Abstracts;
using CleanArcitecture.Service.Dtos;
using CleanArcitecture.Service.Dtos.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArcitecture.Core.Features.Authorization.Queries.Handler
{
    public class AuthorizationHandler(IAuthorizeService authorizeService,
                                      UserManager<User> userManager,
                                      RoleManager<IdentityRole<int>> roleManager)
         : ResponseHandler,
        IRequestHandler<GetUserRolesList, Response<UserRolesListDto>>,
        IRequestHandler<GetUserClaimsList, Response<UserClaimsListDto>>
    {
        private readonly IAuthorizeService _authorizeService = authorizeService;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

        public async Task<Response<UserRolesListDto>> Handle(GetUserRolesList request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return
                    BadRequest<UserRolesListDto>("User Not Found");
            var userRoles = await _authorizeService.GetUserRoles(user);

            return Success(userRoles);
        }

        public async Task<Response<UserClaimsListDto>> Handle(GetUserClaimsList request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return
                    BadRequest<UserClaimsListDto>("User Not Found");
            var userClaims = await _authorizeService.GetUserClaims(user);

            return Success(userClaims);
        }
    }
}
