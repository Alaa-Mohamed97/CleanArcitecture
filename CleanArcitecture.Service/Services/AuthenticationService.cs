using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Infrastructure.Context;
using CleanArcitecture.Service.Abstracts;
using CleanArcitecture.Service.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArcitecture.Service.Services
{
    public class AuthenticationService(IOptions<JwtSettings> jwtSettings,
        IRefreshTokenRepository refreshTokenRepository,
        UserManager<User> userManager,
        AppDBContext appDBContext,
        IEmailService emailService) : IAuthenticationService
    {
        private readonly IOptions<JwtSettings> _jwtSettings = jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly UserManager<User> _userManager = userManager;
        private readonly AppDBContext _appDBContext = appDBContext;
        private readonly IEmailService _emailService = emailService;

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.Value.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.Token,
                Token = accessToken,
                UserId = user.Id
            };
            // add refresh token to DB
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult();
            response.RefreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;

        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserName",user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber!),
                 new Claim("Id", user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserName")!.Value;
            refreshTokenResult.Token = refreshToken;
            refreshTokenResult.ExpireDate = (DateTime)expiryDate;
            response.RefreshToken = refreshTokenResult;
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.Value.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Value.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.Value.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Value.Secret)),
                ValidAudience = _jwtSettings.Value.Audience,
                ValidateAudience = _jwtSettings.Value.ValidateAudience,
                ValidateLifetime = _jwtSettings.Value.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireDate = DateTime.Now.AddDays(_jwtSettings.Value.RefreshTokenExpireDate),
                UserName = username,
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Value.Issuer,
                _jwtSettings.Value.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.Value.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Value.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        public async Task<string> SendResetPasswordCode(string email)
        {
            var transaction = await _appDBContext.Database.BeginTransactionAsync();
            try
            {
                //get user
                var user = await _userManager.FindByEmailAsync(email);
                //check user is exist
                if (user == null)
                    return "UserNotFound";
                //generate code
                Random rnd = new Random();
                var otpNumber = rnd.Next(0, 100000).ToString("D6");
                //update user
                user.VerificationCode = otpNumber;
                await _userManager.UpdateAsync(user);
                // send email
                var message = "Your OTP number is: " + otpNumber;
                await _emailService.SendEmail(user.Email!, message, "Verification Code To Reset Password");
                await transaction.CommitAsync();
                return "Success";
                // must be handle this more by check the result of send email
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmOTP(string code, string email)
        {
            //get user
            var user = await _userManager.FindByEmailAsync(email);
            //check user is exist
            if (user == null)
                return "UserNotFound";
            //compare user code with code
            if (user.VerificationCode == code)
            {
                var result = await SetUserVerificationCodeToNull(user);
                if (result.Equals("success"))
                    return "success";
                return "failed";
            }
            return "invalidCode";
        }
        public async Task<string> ResetPassword(ResetPasswordDTO resetPassword)
        {
            //get user
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            //check user is exist
            if (user == null)
                return "UserNotFound";
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (result.Succeeded)
                return "success";
            return "failed";
        }
        private async Task<string> SetUserVerificationCodeToNull(User user)
        {
            user.VerificationCode = null;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return "success";
            return "failed";
        }
    }
}
