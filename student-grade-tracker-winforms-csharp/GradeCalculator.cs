namespace StudentGradeTracker.Utilities;

public static class GradeCalculator
{
    public static string GetLetterGrade(double average)
    {
        return average switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }

    public static double CalculateClassAverage(List<Models.Student> students)
    {
        if (students == null || students.Count == 0) return 0;
        return students.Average(s => s.OverallAverage);
    }
}