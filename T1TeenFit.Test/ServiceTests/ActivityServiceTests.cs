using Xunit;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Services;


namespace T1TeenFit.Test.ServiceTests
{
    public class ActivityServiceTests : BaseTestClass
    {

        // Activity service setup using in memory database 
        private readonly ActivityService _activityService;
        public ActivityServiceTests()
        {
            _activityService = new ActivityService(_context);
        }


        [Fact]
        public void GetAllActivities_WhenNone_ShouldReturnNone()
        {
            // Arrange

            // Act 
            var activities = _activityService.GetAllActivities();
            var count = activities.Count;

            // Assert 
            Assert.Equal(0, count);
        }


        [Fact]
        public void GetAllActivities_WhenOneAdded_ShouldReturnOne()
        {
            // Arrange 
            _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            _context.SaveChanges();

            // Act 
            var returnedActivities = _activityService.GetAllActivities();
            var count = returnedActivities.Count;

            // Assert 
            Assert.Equal(1, count);
        }


        [Fact]
        public void GetAllActivities_WhenReturned_ShouldBeOrderedByName()
        {
            // Arrange 
            var activity1 = _context.Activities.Add(new Activity
            {
                Name = "Basketball",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });
            var activity2 = _context.Activities.Add(new Activity
            {
                Name = "Cycling",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            _context.SaveChanges();

            // Act
            var returnedActivities = _activityService.GetAllActivities();

            // Assert
            Assert.Equal(returnedActivities.First().Name, activity1.Entity.Name);
            Assert.Equal(returnedActivities.Last().Name, activity2.Entity.Name);
        }


        [Fact]
        public void GetActivityById_WhenNone_ShouldReturnNull()
        {
            // Arrange

            // Act 
            var activity = _activityService.GetActivityById(1);

            // Assert 
            Assert.Null(activity);
        }


        [Fact]
        public void GetActivityById_WhenActivityExists_ShouldReturnActivity()
        {
            // Arrange
            var a = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            _context.SaveChanges();

            // Act
            var activity = _activityService.GetActivityById(a.Entity.Id);

            // Assert
            Assert.NotNull(activity);
            Assert.Equal(a.Entity.Id, activity.Id);
        }


        [Fact]
        public void SearchActivityByName_WhenNotFound_ShouldReturnNull()
        {
            // Arrange
           
            // Act - retrieve activity

            var activity = _activityService.SearchActivity("Basketball");

            // Assert - verify that the activity id doesn't exist
            Assert.Null(activity);

        }


        [Fact]
        public void SearchActivityByName_WhenExists_ShouldReturnActivity()
        {
            // Arrange
            var a = _context.Activities.Add(new Activity
            {
                Name = "Basketball",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            _context.SaveChanges();

            // Act 
            var activity = _activityService.SearchActivity("Basketball");

            // Assert 
            Assert.Equal(activity.Name, a.Entity.Name);
        }
    }
}