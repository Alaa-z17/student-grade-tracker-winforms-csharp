using System.Text;
using StudentGradeTracker.Interfaces;
using StudentGradeTracker.Models;
using StudentGradeTracker.Services;

namespace StudentGradeTracker.Reports;

public sealed class FinalReport : IExportable
{
    public DateTime GeneratedAt { get; set; }
    public List<Student> Students { get; set; } = new();

    public string ExportToCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Student Name,Overall Average,Letter Grade");
        foreach (var s in Students)
        {
            string name = s?.Name ?? "Unknown";
            double avg = s?.OverallAverage ?? 0;
            sb.AppendLine($"{name},{avg:F2},{GradeCalculator.GetLetterGrade(avg)}");
        }
        return sb.ToString();
    }
}