using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Role.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Queries.Models
{
    public class GetRoleDetails : IRequest<Response<RoleDetailsDto>>
    {
        public int Id { get; set; }
        public GetRoleDetails(int Id)
        {
            this.Id = Id;
        }
    }
}
