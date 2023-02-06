namespace AspNetCoreIdentityApp.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetEmailLink, string ToEmail);
    }
}
