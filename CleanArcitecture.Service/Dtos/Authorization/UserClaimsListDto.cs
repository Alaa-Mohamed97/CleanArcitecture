namespace CleanArcitecture.Service.Dtos.Authorization
{
    public class UserClaimsListDto
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
    public class UserClaims
    {
        public bool Value { get; set; }
        public string Type { get; set; }
    }
}
