namespace jwtauth;

public interface IMailOTP
{
    void SendOTP(string email, string oTP);
}
