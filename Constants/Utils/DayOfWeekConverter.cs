namespace Constants.Utils;

public static class DayOfWeekConverter
{
    public static int DayOfWeekFromFrench(string libelle)
    {
        return libelle.ToLower() switch
        {
            "lundi" => (int) DayOfWeek.Monday,
            "mardi" => (int) DayOfWeek.Tuesday,
            "mercredi" => (int) DayOfWeek.Wednesday,
            "jeudi" => (int) DayOfWeek.Thursday,
            "vendredi" => (int) DayOfWeek.Friday,
            "samedi" => (int) DayOfWeek.Saturday,
            "dimanche" => (int) DayOfWeek.Sunday,
            _ => 0
        };
    }
}
