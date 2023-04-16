using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using T1TeenFit.Core.Models;

namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    public enum AlertType { success, warning, information }

    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            var userId = _userManager.GetUserId(User);
            return userId;
        }

        // Alert notification message to be stored as TempData, information enum set as default alert type if not specified  
        public void Alert(string notification, AlertType type = AlertType.information)
        {
            TempData["Alert.Message"] = notification;
            TempData["Alert.Type"] = type.ToString();
        }


    }
}
