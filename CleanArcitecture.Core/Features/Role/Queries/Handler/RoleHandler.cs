using AutoMapper;
using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Role.Queries.DTOs;
using CleanArcitecture.Core.Features.Role.Queries.Models;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Queries.Handler
{
    public class RoleHandler(IAuthorizeService authorizeService,
                            IMapper mapper)
        : ResponseHandler,
        IRequestHandler<GetRoleList, Response<List<RoleListDto>>>,
        IRequestHandler<GetRoleDetails, Response<RoleDetailsDto>>
    {
        private readonly IAuthorizeService _authorizeService = authorizeService;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<RoleListDto>>> Handle(GetRoleList request, CancellationToken cancellationToken)
        {
            var roles = await _authorizeService.GetRoles();
            var result = _mapper.Map<List<RoleListDto>>(roles);
            return Success(result);
        }

        public async Task<Response<RoleDetailsDto>> Handle(GetRoleDetails request, CancellationToken cancellationToken)
        {
            var role = await _authorizeService.GetRoleById(request.Id);
            var result = _mapper.Map<RoleDetailsDto>(role);
            return Success(result);
        }
    }
}
