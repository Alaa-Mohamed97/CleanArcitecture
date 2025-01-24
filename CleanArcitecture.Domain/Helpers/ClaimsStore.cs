using System.Security.Claims;

namespace CleanArcitecture.Domain.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Student","False"),
            new Claim("Update Student","False"),
            new Claim("Delete Student","False"),
        };
    }
}
