using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedRole(RoleManager<IdentityRole<int>> _roleManager)
        {
            var roles = await _roleManager.Roles.CountAsync();
            if (roles <= 0)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "User"
                });
            }
        }
    }
}
