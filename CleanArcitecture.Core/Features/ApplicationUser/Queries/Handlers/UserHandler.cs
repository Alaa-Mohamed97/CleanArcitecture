using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.DTOs;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.Models;
using CleanArcitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserHandler(UserManager<User> userManager, IMapper mapper) : ResponseHandler,
        IRequestHandler<GetUserListQuery, Response<List<UserListDto>>>,
        IRequestHandler<GetUserDetails, Response<UserDetailsDto>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<UserListDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userManager.Users
                                        .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return Success(userList, null, new { count = userList.Count() });

        }

        public async Task<Response<UserDetailsDto>> Handle(GetUserDetails request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request._Id);
            if (user == null)
                return NotFound<UserDetailsDto>();
            var result = _mapper.Map<UserDetailsDto>(user);
            return Success(result);

        }
    }
}
