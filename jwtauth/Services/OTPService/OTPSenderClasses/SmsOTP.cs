namespace jwtauth;

public class SmsOTP : ISmsOTP
{
    private readonly ISmsSender _sender;
    public string Subject { get; private set; }
    public SmsOTP(string subject, ISmsSender sender)
    {
        Subject = subject;
        _sender = sender;
    }

    public void SendOTP(string mobile, string oTP)
    {
        string message = $"your {Subject} OTP is {oTP}";

        _sender.SendSms(mobile, message);
    }
}
