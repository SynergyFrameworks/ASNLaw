using LawUI.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LawUI.Services;

public class UserClaimService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserClaimService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateUserClaimsAsync(ApplicationUser user, bool isSubscribed, bool isPaid, DateTime expirationDate)
    {
        var claims = new List<Claim>
    {
        new Claim("Subscribed", isSubscribed.ToString()),
        new Claim("Paid", isPaid.ToString()),
        new Claim("Expiration", expirationDate.ToString("o"))
    };

        var existingClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in claims)
        {
            var existingClaim = existingClaims.FirstOrDefault(c => c.Type == claim.Type);
            if (existingClaim != null)
            {
                await _userManager.ReplaceClaimAsync(user, existingClaim, claim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, claim);
            }
        }
    }
}
