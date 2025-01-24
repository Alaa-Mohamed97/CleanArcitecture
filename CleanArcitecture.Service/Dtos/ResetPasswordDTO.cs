namespace CleanArcitecture.Service.Dtos
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
