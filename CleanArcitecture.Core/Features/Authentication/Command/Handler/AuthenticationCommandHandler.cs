using CleanArcitecture.Core.Base;
using CleanArcitecture.Core.Features.Authentication.Command.Models;
using CleanArcitecture.Core.SharedResources;
using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArcitecture.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler(UserManager<User> userManager,
          IStringLocalizer<GlobalMessages> stringLocalizer,
          SignInManager<User> signInManager,
          IAuthenticationService authenticationService) : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
        IRequestHandler<SendResetPasswordCodeCommand, Response<string>>,
        IRequestHandler<ConfirmOTPCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IStringLocalizer<GlobalMessages> _stringLocalizer = stringLocalizer;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return NotFound<JwtAuthResult>(_stringLocalizer.GetString(GlobalMessages.NotFound));
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPassword.Succeeded)
                return BadRequest<JwtAuthResult>("Password not correct ");
            var token = await _authenticationService.GetJWTToken(user);
            return Success(token);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>("Algorithm Is Wrong");
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>("Token Is NotExpired");
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>("Refresh Token Is Not Found");
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>("RefreshTokenIsExpired");
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            return result switch
            {
                "UserNotFound" => NotFound<string>("User Not Found"),
                "Failed" => BadRequest<string>("Send Reset Password Code Failed"),
                _ => Success(result)
            };
        }

        public async Task<Response<string>> Handle(ConfirmOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmOTP(request.Code, request.Email);
            return result switch
            {
                "UserNotFound" => NotFound<string>("User Not Found"),
                "invalidCode" => BadRequest<string>("invalid Code, Please try again"),
                "Failed" => BadRequest<string>("An Error Occurred when confirm your code, Please try again"),
                _ => Success(result)
            };
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request);
            return result switch
            {
                "UserNotFound" => NotFound<string>("User Not Found"),
                "Failed" => BadRequest<string>("An Error Occurred when reset Password, Please try again"),
                _ => Success(result)
            };
        }
    }
}
