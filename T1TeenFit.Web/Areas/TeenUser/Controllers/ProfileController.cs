using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using T1TeenFit.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    [Area("TeenUser")]
    public class ProfileController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _userManager = userManager;
        }


        public async Task <IActionResult> Index()
        {
            ApplicationUser teenUser;

            teenUser = await _userManager.GetUserAsync(User);

            return View(teenUser);
        }

    }
}