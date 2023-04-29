namespace jwtauth;

public class TwilioOptionsSetup : OptionSetup<TwilioOptions>
{
    public TwilioOptionsSetup(IConfiguration configuration, string sectionName = "Twilio")
        : base(configuration, sectionName) { }
}
