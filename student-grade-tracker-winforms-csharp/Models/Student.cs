using System.Text;
using System.Text.Json.Serialization;
using StudentGradeTracker.Interfaces;
using StudentGradeTracker.Services;
using System.Threading;

namespace StudentGradeTracker.Models;

public class Student : BasePerson, IGradable, IExportable
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public List<Subject> Subjects { get; set; }

    public Student(string name) : base(name)
    {
        Id = Interlocked.Increment(ref _nextId);
        Subjects = new List<Subject>();
    }

    [JsonConstructor]
    public Student(int id, string name) : base(name)
    {
        Id = id;
        int current;
        do
        {
            current = _nextId;
            if (id < current) break;
        } while (Interlocked.CompareExchange(ref _nextId, id + 1, current) != current);
        Subjects = new List<Subject>();
    }

    public double OverallAverage => Subjects.Count == 0 ? 0 : Subjects.Average(s => s.AverageGrade);
    public string LetterGrade => GradeCalculator.GetLetterGrade(OverallAverage);

    public override string GetInfo() => $"{Name} – Average: {OverallAverage:F2} ({LetterGrade})";
    public double GetAverage() => OverallAverage;
    public string GetLetterGrade() => LetterGrade;

    public string ExportToCsv()
    {
        var sb = new StringBuilder();
        foreach (var subject in Subjects)
            foreach (var grade in subject.Grades)
                sb.AppendLine($"{Name},{subject.Name},{grade.Value}");
        return sb.ToString();
    }

    public class GradeBreakdown
    {
        public string SubjectName { get; set; } = string.Empty;
        public double Average { get; set; }
        public List<double> Grades { get; set; } = new();
    }

    public List<GradeBreakdown> GetBreakdown() =>
        Subjects.Select(s => new GradeBreakdown
        {
            SubjectName = s.Name,
            Average = s.AverageGrade,
            Grades = s.Grades.Select(g => g.Value).ToList()
        }).ToList();
}