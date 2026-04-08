namespace StudentGradeTracker.Models;

public class Subject
{
    public string Name { get; set; } = string.Empty;
    public List<double> Grades { get; set; } = new();

    public double Average => Grades.Count == 0 ? 0 : Grades.Average();
}