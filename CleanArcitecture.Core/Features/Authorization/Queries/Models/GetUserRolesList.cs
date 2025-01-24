using CleanArcitecture.Core.Base;
using CleanArcitecture.Service.Dtos;
using MediatR;

namespace CleanArcitecture.Core.Features.Authorization.Queries.Models
{
    public class GetUserRolesList : IRequest<Response<UserRolesListDto>>
    {
        public int UserId { get; set; }
        public GetUserRolesList(int userId)
        {
            UserId = userId;
        }
    }
}
