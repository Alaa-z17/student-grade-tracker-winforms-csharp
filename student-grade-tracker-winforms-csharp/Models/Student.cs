using student_grade_tracker_winforms_csharp.Services;
using StudentGradeTracker.Interfaces;

namespace StudentGradeTracker.Models;

public class Student : BasePerson, IGradable
{
    public int Id { get; set; }
    public List<Subject> Subjects { get; set; } = new();

    // Composition: Student has a list of grades per subject
    public double OverallAverage => Subjects.Count == 0 ? 0 : Subjects.Average(s => s.Average);

    // Nested class example
    public class GradeBreakdown
    {
        public string ?SubjectName { get; set; }
        public double Average { get; set; }
        public string LetterGrade => GradeCalculator.GetLetterGrade(Average);
    }

    public List<GradeBreakdown> GetBreakdown() => Subjects.Select(s => new GradeBreakdown
    {
        SubjectName = s.Name,
        Average = s.Average
    }).ToList();

    public override string GetReport() => $"{Name} (ID: {Id}) - Overall: {OverallAverage:F2}";

    public string GetGradeSummary() => $"Student: {Name}, Average: {OverallAverage:F2}, Grade: {GradeCalculator.GetLetterGrade(OverallAverage)}";
}