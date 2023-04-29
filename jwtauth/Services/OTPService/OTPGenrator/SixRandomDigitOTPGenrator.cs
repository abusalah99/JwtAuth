namespace jwtauth;

public class SixRandomDigitOTPGenrator : IOTPGenrator
{
    public string GenrateOTP()
    {
        Random generator = new();
        string OTP = generator.Next(0, 1000000).ToString("D6");

        return OTP;
    }
}
