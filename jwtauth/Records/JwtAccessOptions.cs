namespace jwtauth;

public class JwtAccessOptions
{
    public  string? issuser { get; init; } 
    public  string? Audience { get; init; }
    public  string? SecretKey { get; init; }

}

