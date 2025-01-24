namespace CleanArcitecture.Service.Dtos.Authorization
{
    public class UpdateUserClaimsDto
    {
        public int UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }

    }
}
