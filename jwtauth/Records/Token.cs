using Microsoft.AspNetCore.Identity;

namespace jwtauth;

public record Token
{
    public string? AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiresAtExpires { get; set; }
    public required string Role { get; set; }

}


