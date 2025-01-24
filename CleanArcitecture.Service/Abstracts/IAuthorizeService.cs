using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Service.Dtos;
using CleanArcitecture.Service.Dtos.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CleanArcitecture.Service.Abstracts
{
    public interface IAuthorizeService
    {
        // must move role methods to new service (role service)
        #region role
        Task<string> AddRole(string roleName);
        Task<string> EditRole(EditRoleRequest editRole);
        Task<bool> IsRoleExist(string roleName);
        Task<string> DeleteRole(int Id);
        Task<List<IdentityRole<int>>> GetRoles();
        Task<IdentityRole<int>> GetRoleById(int Id);
        #endregion
        Task<UserRolesListDto> GetUserRoles(User user);
        Task<string> UpdateUserRoles(int userId, List<string> roles);
        Task<UserClaimsListDto> GetUserClaims(User user);
        Task<string> UpdateUserClaims(UpdateUserClaimsDto updateUserClaims);
    }
}
