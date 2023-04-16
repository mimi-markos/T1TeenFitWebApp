using Xunit;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Services;

namespace T1TeenFit.Test.ServiceTests
{
    public class ActivityLogServiceTests : BaseTestClass
    {

        // Activity log service setup using in memory database 
        private readonly ActivityLogService _activityLogService;
        private readonly ActivityService _activityService;
        public ActivityLogServiceTests()
        {
            _activityLogService = new ActivityLogService(_context, _activityService);
        }


        [Fact]
        public void GetAllActivtyLogsByUserId_WhenNone_ShouldReturnNone()
        {
            // Arrange
            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com",
                PasswordHash = "John123*"
            });

            _context.SaveChanges();

            // Act
            var activityLogs = _activityLogService.GetAllActivityLogsByUserId(teenUser.Entity.Id);
            var count = activityLogs.Count;

            // Assert
            Assert.Equal(0, count);
        }


        [Fact]
        public void GetAllActivityLogsByUserId_WhenOneAdded_ShouldReturnOne()
        {
            // Arrange
            var activity = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com"

            });

            _context.SaveChanges();

            _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });

            _context.SaveChanges();

            // Act
            var activityLogs = _activityLogService.GetAllActivityLogsByUserId(teenUser.Entity.Id);
            var count = activityLogs.Count;

            // Assert
            Assert.Equal(1, count);
        }


        [Fact]
        public void GetAllActivityLogsByUserId_WhenReturned_ShouldBeOrderedByDescendingDate()
        {
            // Arrange
            var activity = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com"

            });

            _context.SaveChanges();

            var activityLog1 = _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });

            var activityLog2 = _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = new DateTime(2022, 1, 1),
                Duration = 50,
                GlucoseBeforeActivity = 10.0,
                GlucoseAfterActivity = 10.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });
            _context.SaveChanges();

            // Act
            var activityLogs = _activityLogService.GetAllActivityLogsByUserId(teenUser.Entity.Id);


            // Assert
            Assert.Equal(activityLogs.First().ActivityDate, activityLog1.Entity.ActivityDate);
            Assert.Equal(activityLogs.Last().ActivityDate, activityLog2.Entity.ActivityDate);

        }


        [Fact]
        public void GetActivityLogById_WhenNone_ShouldReturnNone()
        {
            // Arrange

            // Act 
            var activityLog = _activityLogService.GetActivityLogById(1);

            // Assert 
            Assert.Null(activityLog);
        }


        [Fact]
        public void GetActivityLogById_WhenActivityLogExists_ShouldReturnActivityLog()
        {
            // Arrange
            var activity = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com"

            });

            _context.SaveChanges();

            var log = _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });

            _context.SaveChanges();


            // Act
            var activityLog = _activityLogService.GetActivityLogById(log.Entity.Id);

            // Assert
            Assert.NotNull(activityLog);
            Assert.Equal(log.Entity.Id, activityLog.Id);
        }


        [Fact]
        public void AddActivityLog_WhenUniqueId_ShouldSetAllProperties()
        {
            // Arrange
            var activity = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com"

            });

            _context.SaveChanges();

            // Act
            var log = _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });

            _context.SaveChanges();

            var activityLog = _activityLogService.GetActivityLogById(log.Entity.Id);

            // Act
            Assert.NotNull(activityLog);
            Assert.Equal(log.Entity.Id, activityLog.Id);
            Assert.Equal(log.Entity.ActivityDate, activityLog.ActivityDate);
            Assert.Equal(log.Entity.GlucoseBeforeActivity, activityLog.GlucoseBeforeActivity);
            Assert.Equal(log.Entity.GlucoseAfterActivity, activityLog.GlucoseAfterActivity);
            Assert.Equal(log.Entity.ActivityNotes, activityLog.ActivityNotes);
        }


        [Fact]
        public void UpdateActivityLog_WhenExists_ShouldSetAllProperties()
        {
            // Arrange
            var aLog = new ActivityLog()
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
            };
            _context.SaveChanges();

            // Act 
            aLog.ActivityDate = new DateTime(2022, 1, 1);
            aLog.Duration = 60;
            aLog.GlucoseBeforeActivity = 5.0;
            aLog.GlucoseAfterActivity = 5.0;
            aLog.ActivityNotes = "These are Activity Notes";

            _activityLogService.UpdateActivityLog(aLog);

            // Assert
            Assert.NotNull(aLog);
            Assert.Equal(60, aLog.Duration);
            Assert.Equal(5.0, aLog.GlucoseBeforeActivity);
            Assert.Equal(5.0, aLog.GlucoseAfterActivity);
            Assert.Equal("These are Activity Notes", aLog.ActivityNotes);
        }


        [Fact]
        public void DeleteActivityLog_ThatExists_ShouldReturnTrue()
        {
            // Arrange
            var activity = _context.Activities.Add(new Activity
            {
                Name = "Activity",
                Description = "Description",
                Tips = "Tips",
                ImageUrl = "https://url.com"
            });

            var teenUser = _context.ApplicationUsers.Add(new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@app.com"

            });

            _context.SaveChanges();

            var log = _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityDate = DateTime.Now,
                Duration = 10,
                GlucoseBeforeActivity = 5.0,
                GlucoseAfterActivity = 5.0,
                ActivityNotes = "Activity Notes",
                ActivityId = activity.Entity.Id,
                ApplicationUserId = teenUser.Entity.Id
            });

            _context.SaveChanges();

            // Act 
            var deletedLog = _activityLogService.DeleteActivityLog(log.Entity.Id);
            var log1 = _activityLogService.GetActivityLogById(log.Entity.Id);

            // Assert 
            Assert.Null(log1);
            Assert.True(deletedLog);
        }

        [Fact]
        public void DeleteActivityLog_ThatDoesntExist_ShouldReturnFalse()
        {
            // Arrange


            // Act 
            var deletedLog = _activityLogService.DeleteActivityLog(1);

            // Assert
            Assert.False(deletedLog);
        }
    }
}