namespace jwtauth;

public interface IMailSender
{
    void SendMail(string mailAddress, string subject, string message);
}
