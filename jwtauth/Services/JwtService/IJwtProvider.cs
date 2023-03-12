namespace jwtauth;

public interface IJwtProvider
{
    string GenrateAccessToken(User user);
    string GenrateRefreshToken();

}
