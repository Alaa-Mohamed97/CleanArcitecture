namespace CleanArcitecture.Domain.Helpers
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; } = null!;
        public RefreshToken RefreshToken { get; set; } = null!;
    }
    public class RefreshToken
    {
        public string Token { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
    }
}
