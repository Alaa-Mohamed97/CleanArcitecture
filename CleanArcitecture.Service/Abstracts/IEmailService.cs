namespace CleanArcitecture.Service.Abstracts
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string message, string subject);
    }
}
