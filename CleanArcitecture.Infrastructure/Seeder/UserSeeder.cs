using CleanArcitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedUser(UserManager<User> _userManager)
        {
            var users = await _userManager.Users.CountAsync();
            if (users <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "012002365",
                    FullName = "Admin"
                };
                await _userManager.CreateAsync(defaultUser, "Aa@12345");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
