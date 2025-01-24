using CleanArcitecture.Api.Base;
using CleanArcitecture.Core.Features.Role.Command.Models;
using CleanArcitecture.Core.Features.Role.Queries.Models;
using CleanArcitecture.Domain.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : AppBaseController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet(Router.RoleRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new GetRoleList());
            return NewResult(response);
        }
        [HttpPost(Router.RoleRouting.GetById)]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _mediator.Send(new GetRoleDetails(Id));
            return NewResult(response);
        }
        [HttpPost(Router.RoleRouting.Create)]
        public async Task<IActionResult> Create(AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.RoleRouting.Edit)]
        public async Task<IActionResult> Edit(EditRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.RoleRouting.Delete)]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(Id));
            return NewResult(response);
        }
    }
}
