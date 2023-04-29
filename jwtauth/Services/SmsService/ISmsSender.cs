namespace jwtauth;

public interface ISmsSender
{
    void SendSms(string mobileNumber, string message);
}