using System.Text;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Services;

public static class CsvExportService
{
    public static void ExportToCsv(List<Student> students, string filePath)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Student Name,Subject,Grade");

        foreach (var student in students)
        {
            if (student.Subjects.Count == 0)
            {
                sb.AppendLine($"{EscapeCsvField(student.Name)},,0");
                continue;
            }

            foreach (var subject in student.Subjects)
            {
                if (subject.Grades.Count == 0)
                {
                    sb.AppendLine($"{EscapeCsvField(student.Name)},{EscapeCsvField(subject.Name)},0");
                    continue;
                }

                foreach (var grade in subject.Grades)
                {
                    sb.AppendLine($"{EscapeCsvField(student.Name)},{EscapeCsvField(subject.Name)},{grade.Value}");
                }
            }
        }

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    private static string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field)) return "";
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
            return $"\"{field.Replace("\"", "\"\"")}\"";
        return field;
    }
}