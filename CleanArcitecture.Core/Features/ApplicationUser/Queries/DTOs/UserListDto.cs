﻿namespace CleanArcitecture.Core.Features.ApplicationUser.Queries.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
