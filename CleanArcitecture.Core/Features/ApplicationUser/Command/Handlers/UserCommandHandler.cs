using AutoMapper;
using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.ApplicationUser.Command.Models;
using CleanArcitecture.Core.SharedResources;
using CleanArcitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArcitecture.Core.Features.ApplicationUser.Command.Handlers
{
    public class UserCommandHandler(UserManager<User> userManager,
        IMapper mapper,
        IStringLocalizer<GlobalMessages> stringLocalizer) :
        ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region fields
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<GlobalMessages> _stringLocalizer = stringLocalizer;

        #endregion
        #region constractor
        #endregion
        #region methods
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //check if email is exist
            var isEmailExist = await _userManager.FindByEmailAsync(request.Email);
            if (isEmailExist != null)
            {
                return BadRequest<string>(_stringLocalizer.GetString(GlobalMessages.Email + " " + GlobalMessages.IsExist));
            }
            //check if userName is exist
            var isUserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (isUserNameExist != null)
            {
                return BadRequest<string>(_stringLocalizer.GetString(GlobalMessages.UserName + " " + GlobalMessages.IsExist));
            }
            //mapping
            var user = _mapper.Map<User>(request);
            //create
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer.GetString(GlobalMessages.User + " " + GlobalMessages.NotAdded));
            }
            //add role to user
            var users = await _userManager.Users.ToListAsync();
            if (users.Count > 0)
            {

                await _userManager.AddToRoleAsync(user, "Test");
            }
            else
            {

                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return Success<string>(_stringLocalizer.GetString(GlobalMessages.User + " " + GlobalMessages.Added));

        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //get user
            var getUser = await _userManager.FindByIdAsync(request.Id.ToString());
            // check is uer found
            if (getUser == null)
                return NotFound<string>(_stringLocalizer.GetString(GlobalMessages.NotFound));

            //mapping
            var user = _mapper.Map(request, getUser);
            //update
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Success("updated");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //get user
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //check user is found
            if (user == null)
                return NotFound<string>(_stringLocalizer.GetString(GlobalMessages.NotFound));
            //delete user
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Deleted<string>();
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //check user is found
            if (user == null)
                return NotFound<string>(_stringLocalizer.GetString(GlobalMessages.NotFound));
            //change password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
                return Success("Password changed Successfully");
            return BadRequest<string>();
        }
        #endregion
    }
}
