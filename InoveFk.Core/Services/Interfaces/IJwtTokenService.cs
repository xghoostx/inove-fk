using InoveFk.Core.Base;
using System.Security.Claims;

namespace InoveFk.Core.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
    public string GenerateTokenForService(string serviceName);
    (bool isValid, ClaimsPrincipal claimsPrincipal) ValidateToken(string token);
}
