using T1TeenFit.Core.Models;

namespace T1TeenFit.Core.IServices
{
    // Interface describes the methods that the ActivityService class should implement 
    public interface IActivityService
    {
        IList<Activity> GetAllActivities();
        Activity GetActivityById(int id);
        Activity SearchActivity(string activityName);
    }
}