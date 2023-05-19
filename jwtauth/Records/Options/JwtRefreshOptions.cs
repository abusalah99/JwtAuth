namespace jwtauth;

public record JwtRefreshOptions 
{
    public string? SecretKey { get; init; }
    public int ExpireTimeInMonths { get; init; }
}