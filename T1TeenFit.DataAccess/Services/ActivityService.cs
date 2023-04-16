using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Data;

namespace T1TeenFit.DataAccess.Services
{
    public class ActivityService : IActivityService
    {

        private readonly ApplicationDbContext _context;

        public ActivityService(ApplicationDbContext context)
        {
            _context = context;
        }



        // Method to get all activities in ascending order of name
        public IList<Activity> GetAllActivities()
        {
            var activities = _context.Activities
                                     .OrderBy(a => a.Name)
                                     .ToList();

            return activities;
        }


        // Method to get an activity by id
        public Activity GetActivityById(int id)
        {
            var activity = _context.Activities.FirstOrDefault(activity => activity.Id == id);

            return activity;
        }


        // Method to get an activity by the searched activity name  
        public Activity SearchActivity(string activityName)
        {
            var activities = GetAllActivities();

            var searchResult = activities.Where(a => a.Name.ToLower().Contains(activityName.ToLower()))
                                         .FirstOrDefault();

            return searchResult;
        }
    }
}
