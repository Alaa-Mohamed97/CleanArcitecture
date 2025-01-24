using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArcitecture.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        [EncryptColumn]
        public string? VerificationCode { get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
