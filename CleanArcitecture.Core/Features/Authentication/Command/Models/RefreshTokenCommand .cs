using CleanArcitecture.Core.Base;
using CleanArcitecture.Domain.Helpers;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Command.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
