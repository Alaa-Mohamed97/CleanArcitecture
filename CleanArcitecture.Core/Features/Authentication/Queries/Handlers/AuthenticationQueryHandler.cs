using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Authentication.Queries.Models;
using CleanArcitecture.Core.SharedResources;
using CleanArcitecture.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArcitecture.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler(IStringLocalizer<GlobalMessages> stringLocalizer,
                                      IAuthenticationService authenticationService) :
                                      ResponseHandler,
                                      IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IStringLocalizer<GlobalMessages> _stringLocalizer = stringLocalizer;
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>("Token Is Expired");
        }
    }
}
