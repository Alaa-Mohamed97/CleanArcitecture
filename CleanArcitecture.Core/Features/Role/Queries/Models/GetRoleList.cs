using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Role.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Queries.Models
{
    public class GetRoleList : IRequest<Response<List<RoleListDto>>>
    {
    }
}
