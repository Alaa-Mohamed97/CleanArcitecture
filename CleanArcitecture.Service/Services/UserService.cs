using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArcitecture.Service.Services
{
    public class UserService(HttpContextAccessor httpContextAccessor, UserManager<User> userManager) : IUserService
    {
        private readonly HttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<User> GetCurrentUser()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException();
            return user;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userId == null)
                throw new UnauthorizedAccessException();
            return userId;
        }
    }
}
