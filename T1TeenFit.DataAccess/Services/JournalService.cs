using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Data;

namespace T1TeenFit.DataAccess.Services
{
    public class JournalService : IJournalService
    {
        private readonly ApplicationDbContext _context;

        public JournalService(ApplicationDbContext context)
        {
            _context = context;
        }



        // Method to get all journals associated with user id, in descending order of activity date 
        public IList<Journal> GetAllJournalsByUserId(string userId)
        {
            var journals = _context.Journals
                                   .Where(journal => journal.ApplicationUserId == userId)
                                   .OrderByDescending(journal => journal.DateCreated)
                                   .ToList();
            
            return journals;
        }


        // Method to get journal entry by id
        public Journal GetJournalById(int id)
        {
            var journalEntry = _context.Journals.FirstOrDefault(entry => entry.Id == id);
            return journalEntry;
        }


        // Method to add journal entry 
        public Journal AddJournal(Journal entry)
        {
            var journalEntry = GetJournalById(entry.Id);

            if (journalEntry != null)
            {
                return null;
            }

            _context.Journals.Add(entry);
            _context.SaveChanges();

            return entry;
        }


        // Method to update an existing journal entry
        public bool UpdateJournal(Journal entry)
        {
            var journalEntry = GetJournalById(entry.Id);
            if (journalEntry == null)
            {
                return false;
            }

            journalEntry.Title = entry.Title;
            journalEntry.JournalMessage = entry.JournalMessage;
            journalEntry.DateCreated = entry.DateCreated;
            journalEntry.Mood = entry.Mood;

            _context.SaveChanges();

            return true;
        }


        // Method to delete an existing journal entry 
        public bool DeleteJournal(int id)
        {
            var journalEntry = GetJournalById(id);
            if (journalEntry == null)
            {
                return false;
            }

            _context.Journals.Remove(journalEntry);
            _context.SaveChanges();

            return true;
        }
    }
}
