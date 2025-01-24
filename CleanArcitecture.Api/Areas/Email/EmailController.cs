using CleanArcitecture.Api.Base;
using CleanArcitecture.Core.Features.Email.Command.Models;
using CleanArcitecture.Domain.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.Email
{
    public class EmailController : AppBaseController
    {
        public EmailController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost(Router.EmailRouting.SendEmail)]
        public async Task<IActionResult> SendEmail(SendEmailCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
