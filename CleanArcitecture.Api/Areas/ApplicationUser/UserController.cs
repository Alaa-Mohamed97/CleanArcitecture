using CleanArcitecture.Api.Base;
using CleanArcitecture.Core.Features.ApplicationUser.Command.Models;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.Models;
using CleanArcitecture.Domain.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.ApplicationUser
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : AppBaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create(AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> Edit(EditUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(Id));
            return NewResult(response);
        }
        [HttpDelete(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.UserRouting.List)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetUserListQuery());
            return NewResult(response);
        }
        [HttpPost(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetUserDetails(Id));
            return NewResult(response);
        }
    }
}
