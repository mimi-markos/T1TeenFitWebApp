using T1TeenFit.Core.Models;

namespace T1TeenFit.Core.IServices
{
    // Interface describes the methods that the PersonaService class should implement 
    public interface IPersonaService
    {
        Persona InfoAboutPersona();
        Persona WeeklyGoalFeedback(string userId);
        Persona BeforeActivityLogFeedback(ActivityLog activityLog);
        Persona AfterActivityLogFeedback(ActivityLog activityLog);
        Persona JournalMoodFeedback(Journal journalEntry);

    }
}