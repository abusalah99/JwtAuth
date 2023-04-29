namespace jwtauth;

public class TwilioSmsSender : ISmsSender
{
    private readonly TwilioOptions _options;

    public TwilioSmsSender(IOptions<TwilioOptions> options) => _options = options.Value;

    public void SendSms(string mobileNumber, string message)
    {
        TwilioClient.Init(_options.AccountSID, _options.AuthToken);
        MessageResource.Create(
           body:message,
           from: new Twilio.Types.PhoneNumber(_options.PhoneNumber),
           to:mobileNumber);
    }
}
