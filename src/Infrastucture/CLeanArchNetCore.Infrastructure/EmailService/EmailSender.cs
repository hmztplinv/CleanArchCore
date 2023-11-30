
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailSender : IEmailSender
{
    public EmailSettings _emailSettings { get; }
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task<bool> SendEmail(EmailMessage emailMessage)
    {
        var client=new SendGridClient(_emailSettings.ApiKey);
        var to=new EmailAddress(emailMessage.To);
        var from=new EmailAddress
        {
            Email=_emailSettings.FromAddress,
            Name=_emailSettings.FromName
        };
        var message=MailHelper.CreateSingleEmail(from, to, emailMessage.Subject, emailMessage.Body, emailMessage.Body);
        var response= await client.SendEmailAsync(message);

        // if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
        //     response.StatusCode == System.Net.HttpStatusCode.OK)
        // {
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }

        return response.IsSuccessStatusCode;
    }
}
