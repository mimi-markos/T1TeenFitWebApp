using T1TeenFit.Core.Models;

namespace T1TeenFit.Core.IServices
{
    // Interface describes the methods that the JournalService class should implement 
    public interface IJournalService
    {
        IList<Journal> GetAllJournalsByUserId(string userId);
        Journal GetJournalById(int id);
        Journal AddJournal(Journal entry);
        bool UpdateJournal(Journal entry);
        bool DeleteJournal(int id);
    }
}