using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using T1TeenFit.Core.Models;
using T1TeenFit.Core.UserRoles;

namespace T1TeenFit.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserAccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string order = null)
        {
            var teenUsers = _userManager.GetUsersInRoleAsync(UserRole.Role_TeenUser).Result.ToList();

            if (order == "first name")
            {
                var firstName = teenUsers.OrderBy(f => f.FirstName).ToList();
                return View(firstName);
            }

            if (order == "last name")
            {
                var lastName = teenUsers.OrderBy(f => f.LastName).ToList();
                return View(lastName);
            }

            return View(teenUsers);
        }


        public async Task<IActionResult> Delete()
        {
            ApplicationUser teenUser;

            teenUser = await _userManager.GetUserAsync(User);

            return View(teenUser);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirm()
        //{
        //    var teenUser = await _userManager.GetUserAsync(User);
        //    await _userManager.DeleteAsync(teenUser);

        //    return View("Index");
        //}
    }
}