using Microsoft.EntityFrameworkCore;
using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Data;

namespace T1TeenFit.DataAccess.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IActivityService _activityService;

        public ActivityLogService(ApplicationDbContext context, IActivityService activityService)
        {
            _context = context;
            _activityService = activityService;
        }



        // Method to get all activity logs associated with user id, including activity, in descending order of activity date
        public IList<ActivityLog> GetAllActivityLogsByUserId(string userId)
        {
            var userActivityLogs = _context.ActivityLogs
                                           .Where(aLog => aLog.ApplicationUserId == userId)
                                           .OrderByDescending(aLog => aLog.ActivityDate)
                                           .Include(aLog => aLog.Activity)
                                           .ToList();

            return userActivityLogs;

        }


        // Method to get all activity logs associated with teen user for the current week
        public IList<ActivityLog> GetActivityLogsForCurrentWeekByUserId(string userId, DateTime startDate, DateTime endDate)
        {
            var userActivityLogs = _context.ActivityLogs
                                   .Where(x => x.ApplicationUserId == userId  && (x.ActivityDate >= startDate && x.ActivityDate <= endDate))
                                   .ToList();
            return userActivityLogs;
        }


        // Method to get activity log by id 
        public ActivityLog GetActivityLogById(int id)
        {
            var aLog = _context.ActivityLogs.Include(aLog => aLog.Activity).FirstOrDefault(aLog => aLog.Id == id);
            return aLog;
        }


        // Method to add activity log
        public ActivityLog AddActivityLog(ActivityLog aLog)
        {
            
            var activity = _activityService.GetActivityById(aLog.ActivityId);

            if (activity == null)
            {
                return null;
            }

            _context.ActivityLogs.Add(aLog);
            _context.SaveChanges();

            return aLog;
        }


        // Method to update an existing activity log
        public bool UpdateActivityLog(ActivityLog aLog)
        {
            var activityLog = GetActivityLogById(aLog.Id);
            if (activityLog == null)
            {
                return false;
            }

            activityLog.ActivityDate = aLog.ActivityDate;
            activityLog.Duration = aLog.Duration;
            activityLog.GlucoseBeforeActivity = aLog.GlucoseBeforeActivity;
            activityLog.GlucoseAfterActivity = aLog.GlucoseAfterActivity;
            activityLog.ActivityNotes = aLog.ActivityNotes;

            _context.SaveChanges();

            return true;

        }


        // Method to delete an existing activity log
        public bool DeleteActivityLog(int id)
        {

            var activityLog = GetActivityLogById(id);
            if (activityLog == null)
            {
                return false;
            }
            _context.ActivityLogs.Remove(activityLog);
            _context.SaveChanges();

            return true;
        }
    }
}