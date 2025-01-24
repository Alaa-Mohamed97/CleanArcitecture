using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Email.Command.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;

    }
}
