namespace StudentGradeTracker.Models;

public class Subject
{
    public string Name { get; set; }
    public List<Grade> Grades { get; set; }

    public Subject(string name)
    {
        Name = name;
        Grades = new List<Grade>();
    }

    public double AverageGrade => Grades.Count == 0 ? 0 : Grades.Average(g => g.Value);
}