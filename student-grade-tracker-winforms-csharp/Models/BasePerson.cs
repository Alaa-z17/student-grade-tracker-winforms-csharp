namespace StudentGradeTracker.Models;

public abstract class BasePerson
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public abstract string GetReport();
}