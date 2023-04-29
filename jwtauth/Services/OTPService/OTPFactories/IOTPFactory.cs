namespace jwtauth;

public interface IOTPFactory
{
    IMailOTP MailConfirmationOTP(IMailSender sender);
    IMailOTP MailRestOTP(IMailSender sender);
    ISmsOTP SmsConfirmationOTP(ISmsSender sender);
    ISmsOTP SmsRestOTP(ISmsSender sender);
}
