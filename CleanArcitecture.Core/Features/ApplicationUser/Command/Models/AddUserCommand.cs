﻿using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.ApplicationUser.Command.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
