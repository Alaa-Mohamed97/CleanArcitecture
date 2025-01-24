using CleanArcitecture.Core.Base;
using CleanArcitecture.Service.Dtos.Authorization;
using MediatR;

namespace CleanArcitecture.Core.Features.Authorization.Queries.Models
{
    public class GetUserClaimsList : IRequest<Response<UserClaimsListDto>>
    {
        public int UserId { get; set; }
        public GetUserClaimsList(int userId)
        {
            UserId = userId;
        }
    }
}
