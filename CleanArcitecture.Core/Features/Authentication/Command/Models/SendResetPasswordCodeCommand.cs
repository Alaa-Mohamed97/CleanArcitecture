using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Command.Models
{
    public class SendResetPasswordCodeCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
