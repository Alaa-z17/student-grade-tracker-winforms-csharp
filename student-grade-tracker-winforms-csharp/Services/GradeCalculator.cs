namespace StudentGradeTracker.Services;

public static class GradeCalculator
{
    public static string GetLetterGrade(double average)
    {
        if (average >= 90) return "A";
        if (average >= 80) return "B";
        if (average >= 70) return "C";
        if (average >= 60) return "D";
        return "F";
    }

    public static double CalculateAverage(List<double> grades)
    {
        if (grades == null || grades.Count == 0) return 0;
        return grades.Average();
    }
}