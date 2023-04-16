using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;

namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    [Area("TeenUser")]
    public class DashboardController : BaseController
    {
        private readonly IActivityLogService _activityLogService;
        private readonly IPersonaService _personaService;


        // Weekly physical activity goal
        const double weeklyGoal = 420.0;

        public DashboardController(IActivityLogService activityLogService, IPersonaService personaService, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _activityLogService = activityLogService;
            _personaService = personaService;
        }


        public IActionResult Index()
        {
            var infoAboutPersona = _personaService.InfoAboutPersona();
            if (infoAboutPersona != null)
            {
               ViewBag.AboutPersonaImageUrl = infoAboutPersona.ImageUrl; 
               ViewBag.AboutPersonaMessage = infoAboutPersona.Message;
            }

            var userId = GetCurrentUserId();
            var weeklyGoalFeedbackPersona = _personaService.WeeklyGoalFeedback(userId);

            if (weeklyGoalFeedbackPersona != null)
            {
                ViewBag.WeeklyGoalPersonaImageUrl = weeklyGoalFeedbackPersona.ImageUrl;
                ViewBag.WeeklyGoalPersonaMessage = weeklyGoalFeedbackPersona.Message;
            }

            return View();
        }


        [HttpGet]
        public JsonResult GetWeeklyGoalPercentage()
        {
            var weekRange = GetWeekRange();
            var activityLogs = _activityLogService.GetActivityLogsForCurrentWeekByUserId(GetCurrentUserId(), weekRange.startDate, weekRange.endDate);
            var totalDuration = activityLogs.Sum(x => x.Duration);
            var percentageAchieved = 100 - ((weeklyGoal - totalDuration) / weeklyGoal) * 100;
            var percentageToAchieve = 100 - percentageAchieved;
            return Json(new { percentageAchieved, percentageToAchieve });
        }


        private (DateTime startDate, DateTime endDate) GetWeekRange()
        {
            var today = DateTime.Now;

            if (today.DayOfWeek.ToString() == "Monday")
            {
                return (today, today.AddDays(6));
            }
            else if (today.DayOfWeek.ToString() == "Tuesday")
            {
                return (today.AddDays(-1), today.AddDays(5));
            }
            else if (today.DayOfWeek.ToString() == "Wednesday")
            {
                return (today.AddDays(-2), today.AddDays(4));
            }
            else if (today.DayOfWeek.ToString() == "Thursday")
            {
                return (today.AddDays(-3), today.AddDays(3));
            }
            else if (today.DayOfWeek.ToString() == "Friday")
            {
                return (today.AddDays(-4), today.AddDays(2));
            }
            else if (today.DayOfWeek.ToString() == "Saturday")
            {
                return (today.AddDays(-5), today.AddDays(1));
            }
            return (today.AddDays(-6), today);
        }
    }
}