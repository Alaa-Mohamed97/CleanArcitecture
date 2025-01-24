using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Command.Models
{
    public class ConfirmOTPCommand : IRequest<Response<string>>
    {
        public string Code { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
