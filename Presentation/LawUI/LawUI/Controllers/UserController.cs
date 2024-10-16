using LawUI.Data;
using LawUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LawUI.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserClaimService _userClaimService;

    public UserController(UserManager<ApplicationUser> userManager, UserClaimService userClaimService)
    {
        _userManager = userManager;
        _userClaimService = userClaimService;
    }

    public async Task<IActionResult> UpdateSubscription(string userId, bool isSubscribed, bool isPaid)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var expirationDate = DateTime.UtcNow.AddDays(30); // Example: 30-day subscription
        await _userClaimService.UpdateUserClaimsAsync(user, isSubscribed, isPaid, expirationDate);

        return RedirectToAction("Index");
    }
}
