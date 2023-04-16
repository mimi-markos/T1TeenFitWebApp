using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    [Area("TeenUser")]
    public class ActivityLogController : BaseController
    {
        private readonly IActivityLogService _activityLogService;
        private readonly IPersonaService _personaService;


        // Configuration via dependency injection
        public ActivityLogController(IActivityLogService svc, UserManager<ApplicationUser> userManager, IPersonaService personaService) : base(userManager)
        {
            _activityLogService = svc;
            _personaService = personaService;
        }


        // GET: /activitylog
        public IActionResult Index(bool addedActivityLog = false, int activityLogId = 0)
        {
            
            // display activity log feedback personas 
            if (addedActivityLog)
            {
                var activityLog = _activityLogService.GetActivityLogById(activityLogId);
                var beforeActivityFeedbackpersona = _personaService.BeforeActivityLogFeedback(activityLog);
                var afterActivityFeedbackPersona = _personaService.AfterActivityLogFeedback(activityLog);
                

                if (beforeActivityFeedbackpersona != null)
                {
                    ViewBag.BeforeActivityPersonaMessage = beforeActivityFeedbackpersona.Message;
                    ViewBag.BeforeActivityPersonaImageUrl = beforeActivityFeedbackpersona.ImageUrl;
                }
                if (afterActivityFeedbackPersona != null)
                {
                    ViewBag.AfterActivityPersonaMessage = afterActivityFeedbackPersona.Message;
                    ViewBag.AfterActivityPersonaImageUrl = afterActivityFeedbackPersona.ImageUrl;
                }
            }

            // retrieve all activity logs associated with user id
            var userId = GetCurrentUserId();
            var aLogs = _activityLogService.GetAllActivityLogsByUserId(userId);

            return View(aLogs);
        }


        // GET: /activitylog/details/{id}
        public IActionResult Details(int id)
        {
            // retrieve activity log by id 
            var aLog = _activityLogService.GetActivityLogById(id);

            // if activity log doesn't exist, display alert notification
            if (aLog == null)
            {
                Alert($"Sorry, that activity log was not found.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display activity log details 
            return View(aLog);
        }


        // GET: /activitylog/create{activityId}
        public IActionResult Create(int activityId)
        {
            // display form to add activity log 
            var aLog = new ActivityLog()
            {
                ActivityId = activityId,
                ActivityDate = DateTime.Now
            };

            return View(aLog);
        }



        // POST: /activitylog/create{activityId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ActivityDate, Duration, GlucoseBeforeActivity, GlucoseAfterActivity, ActivityNotes, ActivityId")] ActivityLog aLog)
        {
            // add activity log
            if (ModelState.IsValid)
            {
                aLog.ApplicationUserId = GetCurrentUserId();
                var savedActivityLog = _activityLogService.AddActivityLog(aLog);

                if (savedActivityLog != null)
                {
                    var userId = GetCurrentUserId();
                    _personaService.BeforeActivityLogFeedback(savedActivityLog);
                }

                // if activity log added, display alert notification
                Alert("Activity log added successfully!", AlertType.success);

                return RedirectToAction(nameof(Index), new { id = aLog.Id, addedActivityLog = true, activityLogId = savedActivityLog.Id });
            }

            // if validation errors, redisplay form
            return View(aLog);

        }


        // GET: activitylog/edit/{Id}
        public IActionResult Edit(int id)
        {
            // verify activity log exists using id
            var aLog = _activityLogService.GetActivityLogById(id);

            if (aLog == null)
            {
                // if activity log doesn't exist, display alert notification
                Alert("Sorry, that activity log doesn't exist.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display form to edit activity log
            return View(aLog);
        }


        // POST: activityLog/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ActivityDate, Duration, GlucoseBeforeActivity, GlucoseAfterActivity, ActivityNotes, ActivityId, Id")] ActivityLog aLog)
        {
            // update activity log
            if (ModelState.IsValid)
            {
                aLog.ApplicationUserId = GetCurrentUserId();
                _activityLogService.UpdateActivityLog(aLog);

                // if activity log updated, display alert notification
                Alert("Activity log updated successfully!", AlertType.success);
                return RedirectToAction(nameof(Index));
            }
            // if validation errors, redisplay form
            return View(aLog);
        }


        // GET: activitylog/delete/{id}
        public IActionResult Delete(int id)
        {
            // retrieve activity log by id 
            var aLog = _activityLogService.GetActivityLogById(id);

            // if activity log doesn't exist, display alert notification
            if (aLog == null)
            {
                Alert("Sorry, that activity log doesn't exist.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display activity log for deletion 
            return View(aLog);
        }


        // POST: activitylog/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            // delete activity log and display alert notification
           _activityLogService.DeleteActivityLog(id);
            Alert($"Activity log deleted successfully!", AlertType.success);

            return RedirectToAction(nameof(Index));
        }
    }
}