using student_grade_tracker_winforms_csharp.Services;
using StudentGradeTracker.Interfaces;
using StudentGradeTracker.Models;
using System.Text;

namespace StudentGradeTracker.Reports;

public sealed class FinalReport : IExportable
{
    public DateTime GeneratedAt { get; set; }
    public List<Student>? Students { get; set; }

    public string ExportToCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Student Name,Overall Average,Letter Grade");

        if (Students == null)
            return sb.ToString();

        foreach (var s in Students)
        {
            string name = s?.Name ?? "No Name";
            double avg = s?.OverallAverage ?? 0;
            sb.AppendLine($"{name},{avg:F2},{GradeCalculator.GetLetterGrade(avg)}");
        }
        return sb.ToString();
    }
}