using T1TeenFit.Core.Enums;
using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using T1TeenFit.DataAccess.Data;

namespace T1TeenFit.DataAccess.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly ApplicationDbContext _context;

        public PersonaService(ApplicationDbContext context)
        {
            _context = context;
        }



        // Method to display persona image and message info about persona
        public Persona InfoAboutPersona()
        {
            var aboutInfoPersona = _context.Personas.FirstOrDefault(p => p.PersonaType == PersonaType.InfoAboutPersona);
            return aboutInfoPersona;
        }


        // Method to display persona image and feedback message based on weekly goal percentage  
        public Persona WeeklyGoalFeedback(string userId)
        {
            var totalActivityMins = _context.ActivityLogs.Where(x => x.ApplicationUserId == userId).Sum(x => x.Duration);
            var weeklyGoalPercentageMet = (totalActivityMins/420) * 100;


            if (weeklyGoalPercentageMet == 100)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.WeeklyGoal100);
            }
            else if (weeklyGoalPercentageMet >= 60 && weeklyGoalPercentageMet < 100)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.WeeklyGoal60OrMore);
            }
            else if (weeklyGoalPercentageMet >= 40 && weeklyGoalPercentageMet < 60)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.WeeklyGoalLessThan60);
            }
            else
            {
                return null;
            }
        }


        // Method to display persona image and feedback message based on before glucose reading input in form 
        public Persona BeforeActivityLogFeedback(ActivityLog recentActitivityLog)
        {
            if (recentActitivityLog.GlucoseBeforeActivity < 4)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.BeforeActivityLow);
            }
            else if (recentActitivityLog.GlucoseBeforeActivity > 4 && recentActitivityLog.GlucoseBeforeActivity <= 12)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.BeforeActivityInRange);
            }
            else 
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.BeforeActivityHigh);
            }
        }


        //Method to display persona image and feedback message based on after glucose reading input in form 
        public Persona AfterActivityLogFeedback(ActivityLog recentActitivityLog)
        {
            if (recentActitivityLog.GlucoseAfterActivity < 4)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.AfterActivityLow);
            }
            else if (recentActitivityLog.GlucoseAfterActivity > 4 && recentActitivityLog.GlucoseAfterActivity <= 10)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.AfterActivityInRange);
            }
            else 
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.AfterActivityHigh);
            }
        }


        //Method to provide feedback based on user mood submitted in journal form 
        public Persona JournalMoodFeedback(Journal journalEntry)
        {
            if (journalEntry.Mood == Mood.Happy)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.JournalMood1);
            }
            else if (journalEntry.Mood == Mood.Neutral)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.JournalMood2);
            }
            else if (journalEntry.Mood == Mood.Sad)
            {
                return _context.Personas.FirstOrDefault(x => x.PersonaType == PersonaType.JournalMood3);
            }
            else
                return null;
        }
    }
}