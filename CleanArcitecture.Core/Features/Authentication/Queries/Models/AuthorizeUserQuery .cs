using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
