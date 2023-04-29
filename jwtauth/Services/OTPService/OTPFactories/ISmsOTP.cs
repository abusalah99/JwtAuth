namespace jwtauth;

public interface ISmsOTP
{
    void SendOTP(string mobile, string oTP);
}
