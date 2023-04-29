namespace jwtauth;

public class GmailSmtpMailSender : IMailSender
{
    private readonly GoogleSmtpOptions _MailOptions;

    public GmailSmtpMailSender(IOptions<GoogleSmtpOptions> mailOptions) => _MailOptions = mailOptions.Value;

    public void SendMail(string mailAddress, string subject, string message)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(_MailOptions.Email);
        mail.Subject = subject;
        mail.To.Add(new MailAddress(mailAddress));
        mail.Body = $"<html><body> {message} </body></html>";
        mail.IsBodyHtml = true;

        SmtpClient smtpClient = new("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(_MailOptions.Email, _MailOptions.Password),
            EnableSsl = true,
        };

        smtpClient.Send(mail);
    }
}
