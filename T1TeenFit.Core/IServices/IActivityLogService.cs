using T1TeenFit.Core.Models;

namespace T1TeenFit.Core.IServices
{
    // Interface describes the methods that the ActivityLogService class should implement 
    public interface IActivityLogService
    {
        IList<ActivityLog> GetAllActivityLogsByUserId(string userId);
        IList<ActivityLog> GetActivityLogsForCurrentWeekByUserId(string userId, DateTime startDate, DateTime endDate);
        ActivityLog GetActivityLogById(int id);
        ActivityLog AddActivityLog(ActivityLog aLog);
        bool UpdateActivityLog(ActivityLog aLog);
        bool DeleteActivityLog(int id);
    }
}