using Xunit;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Services;

namespace T1TeenFit.Test.ServiceTests
{
    public class JournalServiceTests : BaseTestClass
    {
        // Journal service setup using in memory database
        private readonly JournalService _journalService;
        public JournalServiceTests()
        {
            _journalService = new JournalService(_context);
        }


        [Fact]
        public void GetAllJournalsByUserId_WhenNone_ShouldReturnNone()
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
            var journals = _journalService.GetAllJournalsByUserId(teenUser.Entity.Id);
            var count = journals.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void GetAllJournalsByUserId_WhenOneAdded_ShouldReturnOne()
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

            _context.Journals.Add(new Journal
            {
                Title = "Title",
                JournalMessage = "Journal Message",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy,
                ApplicationUserId = teenUser.Entity.Id
            });
            
            _context.SaveChanges();

            // Act
            var journals = _journalService.GetAllJournalsByUserId(teenUser.Entity.Id);
            var count = journals.Count;

            // Assert
            Assert.Equal(1, count);
        }


        [Fact]
        public void GetJournalsByUserId_WhenReturned_ShouldBeOrderedByDescendingDate()
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

            var journalEntry1 = _context.Journals.Add(new Journal
            {
                Title = "Title",
                JournalMessage = "Journal Message",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy,
                ApplicationUserId = teenUser.Entity.Id
            });

            var journalEntry2 = _context.Journals.Add(new Journal
            {
                Title = "Title",
                JournalMessage = "Journal Message",
                DateCreated = new DateTime (2022 , 1, 1),
                Mood = Mood.Happy,
                ApplicationUserId = teenUser.Entity.Id
            });

            _context.SaveChanges();

            // Act 
            var journals = _journalService.GetAllJournalsByUserId(teenUser.Entity.Id);

            // Assert
            Assert.Equal(journals.First().DateCreated, journalEntry1.Entity.DateCreated);
            Assert.Equal(journals.Last().DateCreated, journalEntry2.Entity.DateCreated);
        }


        [Fact]
        public void GetJournalById_WhenNone_ShouldReturnNone()
        {
            // Arrange

            // Act 
            var journal = _journalService.GetJournalById(1);

            // Assert 
            Assert.Null(journal);
        }


        [Fact]
        public void GetJournalById_WhenJournalExists_ShouldReturnJournal()
        {
            // Arrange
            var entry = _context.Journals.Add(new Journal
            {
                Title = "Title",
                JournalMessage = "Journal Message",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy
            });
            _context.SaveChanges();

            // Act
            var journal = _journalService.GetJournalById(entry.Entity.Id);

            //Assert
            Assert.NotNull(journal);
            Assert.Equal(entry.Entity.Id, journal.Id);
        }


        [Fact]
        public void AddJournal_WhenUniqueId_ShouldSetAllProperties()
        {
            // Arrange

            // Act 
            var entry = _context.Journals.Add(new Journal
            {
                Title = "",
                JournalMessage = "",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy
            });
            _context.SaveChanges();
            var journal = _journalService.GetJournalById(entry.Entity.Id);

            // Act
            Assert.NotNull(journal);
            Assert.Equal(entry.Entity.Id, journal.Id);
            Assert.Equal(entry.Entity.Title, journal.Title);
            Assert.Equal(entry.Entity.JournalMessage, journal.JournalMessage);
            Assert.Equal(entry.Entity.DateCreated, journal.DateCreated);
            Assert.Equal(entry.Entity.Mood, journal.Mood);
        }


        [Fact]
        public void UpdateJournal_WhenExists_ShouldSetAllProperties()
        {
            // Arrange
            var journal = new Journal()
            {
                Title = "",
                JournalMessage = "",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy
            };
            _context.SaveChanges();

            // Act
            journal.Title = "Title";
            journal.JournalMessage = "Journal Message";
            journal.DateCreated = new DateTime(2022, 1, 2);
            journal.Mood = Mood.Neutral;

            _journalService.UpdateJournal(journal);

            // Assert
            Assert.NotNull(journal);
            Assert.Equal("Title", journal.Title);
            Assert.Equal("Journal Message", journal.JournalMessage);
            //Assert.Equal({2022, 1, 2}, journal.DateCreated);
            Assert.Equal(Mood.Neutral, journal.Mood);
        }


        [Fact]
        public void DeleteJournal_ThatExists_ShouldReturnTrue()
        {
            // Arrange
            var journal = _context.Journals.Add(new Journal
            {
                Title = "",
                JournalMessage = "",
                DateCreated = DateTime.Now,
                Mood = Mood.Happy
            });
            _context.SaveChanges();

            // Act
            var deletedJournal = _journalService.DeleteJournal(journal.Entity.Id);
            var journal1 = _journalService.GetJournalById(journal.Entity.Id);

            // Assert
            Assert.Null(journal1);
            Assert.True(deletedJournal);
        }


        [Fact]
        public void DeleteJournal_ThatDoesntExist_ShouldReturnFalse()
        {
            // Arrange

            // Act 
            var deletedJournal = _journalService.DeleteJournal(1);

            // Assert
            Assert.False(deletedJournal);
        }
    }
}