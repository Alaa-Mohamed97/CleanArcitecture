using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Infrastructure.Context;
using CleanArcitecture.Service.Abstracts;
using CleanArcitecture.Service.Dtos;
using CleanArcitecture.Service.Dtos.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArcitecture.Service.Services
{
    public class AuthorizeService(RoleManager<IdentityRole<int>> roleManager,
                                  UserManager<User> userManager,
                                  AppDBContext appDBContext
                                  ) :
                                  IAuthorizeService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
        private readonly UserManager<User> _userManager = userManager;
        private readonly AppDBContext _appDBContext = appDBContext;

        public async Task<string> AddRole(string roleName)
        {
            var role = new IdentityRole<int>(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return "success";
            return "failed";
        }

        public async Task<string> DeleteRole(int Id)
        {
            //get role
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            //check if role exist
            if (role == null)
                return "NotFound";
            //check if role assigned to user
            var roleUsers = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (roleUsers.Any())
                return "Used";
            //delete user
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "success";
            return "Failed";
        }

        public async Task<string> EditRole(EditRoleRequest editRole)
        {
            //get role
            var role = await _roleManager.FindByIdAsync(editRole.Id.ToString());
            //check if role not exist
            if (role == null)
                return "NotFound";
            //check if role name is exist before
            bool isRoleNameExist = await IsRoleExist(editRole.Name);
            if (isRoleNameExist)
                return "NameIsExist";
            //add role
            role.Name = editRole.Name;
            var result = await _roleManager.UpdateAsync(role);
            //check if updated successfully
            if (result.Succeeded)
                return "success";
            return "Failed";
        }

        public async Task<IdentityRole<int>> GetRoleById(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            //check if role not exist
            if (role == null)
                return null;
            return role;
        }

        public async Task<List<IdentityRole<int>>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }


        public async Task<bool> IsRoleExist(string roleName)
        {
            var isExist = await _roleManager.RoleExistsAsync(roleName);
            if (isExist)
                return true;
            return false;
        }
        public async Task<UserRolesListDto> GetUserRoles(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
            var userRolesList = new UserRolesListDto();
            userRolesList.UserId = user.Id;
            userRolesList.UserFullName = user.FullName;
            userRolesList.UserRoles = new List<UserRoles>();
            foreach (var item in roles)
            {
                var isUserHasRole = await _userManager.IsInRoleAsync(user, item.Name!);
                userRolesList.UserRoles.Add(new UserRoles()
                {
                    Id = item!.Id,
                    Name = item.Name!,
                    HasRole = isUserHasRole,
                });
            }
            return userRolesList;
        }

        public async Task<string> UpdateUserRoles(int userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return "UserNotFound";

            //get old roles
            var userOldRoles = await _userManager.GetRolesAsync(user);
            //remove old roles for user
            if (userOldRoles.Count > 0)
            {
                var removeOldRoles = await _userManager.RemoveFromRolesAsync(user, userOldRoles);
                if (!removeOldRoles.Succeeded)
                    return "CannotRemoveOldRoles";
            }
            var addRolesToUser = await _userManager.AddToRolesAsync(user, roles);
            if (addRolesToUser.Succeeded)
                return "success";
            return "Failed";
        }
        public async Task<UserClaimsListDto> GetUserClaims(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = ClaimsStore.claims;
            var userClaimsList = new UserClaimsListDto();
            userClaimsList.UserId = user.Id;
            userClaimsList.UserFullName = user.FullName;
            userClaimsList.UserClaims = new List<UserClaims>();
            foreach (var item in claims)
            {
                var isUserHasClaim = userClaims.Any(x => x.Type == item.Type);
                userClaimsList.UserClaims.Add(new()
                {
                    Value = isUserHasClaim,
                    Type = item.Type!,
                });
            }
            return userClaimsList;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsDto updateUserClaims)
        {
            var transaction = await _appDBContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserClaims.UserId.ToString());
                if (user == null)
                    return "UserNotFound";

                //get old Claims
                var userOldClaims = await _userManager.GetClaimsAsync(user);
                //remove old Claims for user
                if (userOldClaims.Count > 0)
                {
                    var removeOldClaims = await _userManager.RemoveClaimsAsync(user, userOldClaims);
                    if (!removeOldClaims.Succeeded)
                        return "CannotRemoveOldClaims";
                }
                var newClaims = updateUserClaims.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var addClaimsToUser = await _userManager.AddClaimsAsync(user, newClaims);
                if (addClaimsToUser.Succeeded)
                {

                    await transaction.CommitAsync();
                    return "success";
                }
                else
                {
                    await transaction.RollbackAsync();
                    return "Failed";
                }
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                return "Failed";
            }

        }
    }
}
