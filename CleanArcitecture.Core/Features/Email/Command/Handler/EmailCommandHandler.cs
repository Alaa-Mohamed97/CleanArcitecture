using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Email.Command.Models;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Email.Command.Handler
{
    public class EmailCommandHandler(IEmailService emailService) : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.Email, request.Message, "");
            if (result == "Success")
                return Success(result);
            return BadRequest<string>();
        }
    }
}
