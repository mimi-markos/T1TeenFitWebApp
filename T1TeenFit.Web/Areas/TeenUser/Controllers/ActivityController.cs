using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;

namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    [Area("TeenUser")]
    public class ActivityController : BaseController
    {

        private readonly IActivityService _activityService;

        // Configuration via dependency injection
        public ActivityController(IActivityService svc, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _activityService = svc;
        }


        // GET: /activity
        public IActionResult Index(string activityName = "")
        {

            IList<Activity> activities = new List<Activity>();

            // retrieve searched activity name and display on user interface
            if (activityName != "")
            {
                ViewBag.searchedActivity = activityName;

                var searchResult = _activityService.SearchActivity(activityName);

                if (searchResult != null)
                    activities = new List<Activity>() { searchResult };
                else
                // if activity doesn't exist, display alert notification 
                {
                    Alert($"Sorry, the activity you searched was not found.", AlertType.warning);
                    return RedirectToAction(nameof(Index));
                }
            }
            // display all activities by default
            else
            {
                activities = _activityService.GetAllActivities();
            }

            return View(activities);

        }
        

        // GET: /activity/details/{id}
        public IActionResult Details(int id)
        {
            // retrieve activity by id 
            var activity = _activityService.GetActivityById(id);

            // if activity doesn't exist, display alert notification 
            if (activity == null)
            {
                Alert($"Sorry, that activity was not found.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display activity details 
            return View(activity);
        }
    }
}
