using Cros5.Models;
using Cros5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Cros5.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly OktaApiService _oktaApiService;

        public ProfileController(IConfiguration configuration)
        {
            _oktaApiService = new OktaApiService(configuration["Okta:apiToken"] ?? "00NvZnD_YeJ_cPpvkHACqK8JSSeD8iEdx7slDA9_GH", configuration);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _oktaApiService.GetUserAsync(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);

            var userClaims = User.Claims ?? new List<Claim>();

            return View(user);
        }
    }
}
