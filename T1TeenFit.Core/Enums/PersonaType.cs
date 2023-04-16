namespace T1TeenFit.Core.Enums
{
    public enum PersonaType
    {

        // Persona to be displayed based on the following:  

        // Information about persona
        InfoAboutPersona = 1,

        // Weekly goal percentage feedback 
        WeeklyGoal100 = 2,
        WeeklyGoal60OrMore = 3,
        WeeklyGoalLessThan60 = 4,

        // Before activity feedback based on user input in activity log form 
        BeforeActivityLow = 5,
        BeforeActivityInRange = 6,
        BeforeActivityHigh = 7,

        // After activity feedback based on user input in activity log form 
        AfterActivityLow = 8,
        AfterActivityInRange = 9,
        AfterActivityHigh = 10,

        // Journal feedback based on user mood input in journal form 
        JournalMood1 = 13,
        JournalMood2 = 14,
        JournalMood3 = 15
    }
}