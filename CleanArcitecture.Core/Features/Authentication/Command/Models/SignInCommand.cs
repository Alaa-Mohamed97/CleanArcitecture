using CleanArcitecture.Core.Base;
using CleanArcitecture.Domain.Helpers;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
