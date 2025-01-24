using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserListQuery : IRequest<Response<List<UserListDto>>>
    {
    }
}
