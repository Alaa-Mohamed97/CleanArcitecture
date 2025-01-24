using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.DTOs;
using MediatR;

namespace CleanArcitecture.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserDetails : IRequest<Response<UserDetailsDto>>
    {
        public int _Id { get; set; }
        public GetUserDetails(int Id)
        {
            _Id = Id;
        }
    }
}
