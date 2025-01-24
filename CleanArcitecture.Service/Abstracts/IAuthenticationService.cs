using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Service.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArcitecture.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string AccessToken);
        Task<string> SendResetPasswordCode(string email);
        Task<string> ConfirmOTP(string code, string email);
        Task<string> ResetPassword(ResetPasswordDTO resetPassword);
    }
}
