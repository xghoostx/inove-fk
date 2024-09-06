using InoveFk.Core.Base;
using InoveFk.Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InoveFk.Core.Service;

public class JwtTokenService(string key) : IJwtTokenService
{
    private readonly string _tokenKey = key;

    public string GenerateToken(ApplicationUser user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_tokenKey));
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user?.Id.ToString()),
                new(ClaimTypes.Name, user?.UserName),
                new(ClaimTypes.Email, user?.Email),                 
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        if(user.FirstAccess)
        {
            tokenDescriptor.Subject.AddClaim(new(Constants.IsFirstAccess, user.FirstAccess.ToString()));
        }

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    public string GenerateTokenForService(string serviceName)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_tokenKey));
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, serviceName),
                new(ClaimTypes.Name, "Inove Root"),
                new(ClaimTypes.Email, "inove-digital@outlook.com"),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        tokenDescriptor.Subject.AddClaim(new("Service", serviceName));

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    public (bool isValid, ClaimsPrincipal claimsPrincipal) ValidateToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.UTF8.GetBytes(_tokenKey);

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        try
        {
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(
                token, 
                tokenValidationParameters, 
                out SecurityToken validatedToken);
            return (true, claimsPrincipal);
        }
        catch (Exception)
        {
            return (false, null);
        }
    }
}