namespace jwtauth;

public class GmailSmtpOptionsSetup : OptionSetup<GoogleSmtpOptions>
{
        public GmailSmtpOptionsSetup(IConfiguration configuration, string sectionName = "GmailSmtp")
            : base(configuration, sectionName) { }

}
