namespace StudentGradeTracker.Models;

public class Grade
{
    public double Value { get; set; }
    public Grade(double value) => Value = value;
}