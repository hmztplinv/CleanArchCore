public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage emailMessage);
}