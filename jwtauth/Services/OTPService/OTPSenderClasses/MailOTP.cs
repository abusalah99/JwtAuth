namespace jwtauth;

public class MailOTP : IMailOTP
{
    private readonly IMailSender _sender;
    public string Subject { get; private set; }
    public MailOTP(string subject, IMailSender sender)
    {
        Subject = subject;
        _sender = sender;
    }
    public void SendOTP(string email, string oTP) => _sender.SendMail(email, Subject, oTP);
}
