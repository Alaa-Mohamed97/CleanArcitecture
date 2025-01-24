using CleanArcitecture.Api.Base;
using CleanArcitecture.Core.Features.Authorization.Command.Modules;
using CleanArcitecture.Core.Features.Authorization.Queries.Models;
using CleanArcitecture.Domain.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppBaseController
    {
        public AuthorizationController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet(Router.AuthorizationRouting.UserRoles)]
        public async Task<IActionResult> UserRoles(int userId)
        {
            var response = await _mediator.Send(new GetUserRolesList(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.UserClaims)]
        public async Task<IActionResult> UserClaims(int userId)
        {
            var response = await _mediator.Send(new GetUserClaimsList(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
