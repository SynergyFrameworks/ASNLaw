using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LawUI.Data;

namespace LawUI.Components.Stores;


public class CustomUserStore : UserStore<ApplicationUser>
{
    public CustomUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
        : base(context, describer)
    {
    }

    public override async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var result = await base.CreateAsync(user, cancellationToken);

        if (result.Succeeded)
        {
            await AddCustomClaimsAsync(user, cancellationToken);
        }

        return result;
    }

    private async Task AddCustomClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("Subscribed", "false"), // Default value
        new Claim("Paid", "false"), // Default value
        new Claim("Expiration", DateTime.UtcNow.AddDays(30).ToString("o")) // Default 30-day expiration
    };

        await AddClaimsAsync(user, claims, cancellationToken);
    }
}
