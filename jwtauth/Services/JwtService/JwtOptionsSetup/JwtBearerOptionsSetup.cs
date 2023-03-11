﻿namespace jwtauth;

public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtAccessOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtAccessOptions> jwtOptions)=>
        _jwtOptions = jwtOptions.Value;


    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }
}
