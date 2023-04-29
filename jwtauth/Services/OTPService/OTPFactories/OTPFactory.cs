namespace jwtauth;

public class OTPFactory : IOTPFactory
{
    public IMailOTP MailConfirmationOTP(IMailSender sender)
        => new MailOTP("Mail Confirmation", sender);

    public IMailOTP MailRestOTP(IMailSender sender)
        => new MailOTP("Reset Password", sender);

    public ISmsOTP SmsConfirmationOTP(ISmsSender sender)
        => new SmsOTP("Mobile Confirmation", sender);

    public ISmsOTP SmsRestOTP(ISmsSender sender)
        => new SmsOTP("Reset Password", sender);
}
