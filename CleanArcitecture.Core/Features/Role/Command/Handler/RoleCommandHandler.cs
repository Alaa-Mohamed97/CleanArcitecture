using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Role.Command.Models;
using CleanArcitecture.Service.Abstracts;
using MediatR;

namespace CleanArcitecture.Core.Features.Role.Command.Handler
{
    public class RoleCommandHandler(IAuthorizeService authorizeService) : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IAuthorizeService _authorizeService = authorizeService;

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            //is role exist
            bool isRoleExist = await _authorizeService.IsRoleExist(request.RoleName);
            if (isRoleExist)
                return BadRequest<string>("Role Is Exist");
            //add role
            var result = await _authorizeService.AddRole(request.RoleName);
            if (string.Equals(result, "success"))
                return Success(result);
            return BadRequest<string>("Role Not Added");
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.EditRole(request);

            return result switch
            {
                "NotFound" => NotFound<string>("Role Not Found"),
                "NameIsExist" => BadRequest<string>("Role name is exist "),
                "Failed" => BadRequest<string>("Role Not Update"),
                _ => Success(result)
            };
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.DeleteRole(request.Id);
            return result switch
            {
                "NotFound" => NotFound<string>("Role Not Found"),
                "Used" => BadRequest<string>("Role Is Assigned to users"),
                "Failed" => BadRequest<string>("Role Not Deleted"),
                _ => Success(result)
            };
        }
    }
}
