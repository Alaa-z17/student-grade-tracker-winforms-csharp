using StudentGradeTracker.Models;
using System.Globalization;
using System.Text;

namespace StudentGradeTracker.Services;

public static class CsvImportService
{
    public static List<Student> ImportFromCsv(string filePath)
    {
        // Auto-detect encoding (UTF-8 with/without BOM, UTF-16 LE/BE, etc.)
        string content;
        using (var reader = new StreamReader(filePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
            content = reader.ReadToEnd();

        // Remove any leftover BOM (though StreamReader should handle it, but safe)
        if (content.Length > 0 && content[0] == '\uFEFF')
            content = content.Substring(1);

        var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 2) return new List<Student>();

        string[] headers = ParseCsvLine(lines[0]);
        int nameIdx = -1, subjectIdx = -1, gradeIdx = -1;

        for (int i = 0; i < headers.Length; i++)
        {
            string h = headers[i].Trim().ToLower();
            // Remove any non-breaking spaces or other invisible control chars
            h = new string(h.Where(c => !char.IsControl(c) && c != 160).ToArray());

            if (h.Contains("student") || h == "name")
                nameIdx = i;
            else if (h.Contains("subject"))
                subjectIdx = i;
            else if (h.Contains("grade") || h == "score")
                gradeIdx = i;
        }

        if (nameIdx == -1 || subjectIdx == -1 || gradeIdx == -1)
        {
            string actualHeaders = string.Join(", ", headers.Select(h => $"\"{h}\""));
            throw new InvalidDataException(
                $"CSV must have Student Name, Subject, and Grade columns.\n" +
                $"Detected headers: {actualHeaders}");
        }

        var studentsDict = new Dictionary<string, Student>();
        int skippedRows = 0;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var parts = ParseCsvLine(lines[i]);
            if (parts.Length <= Math.Max(nameIdx, Math.Max(subjectIdx, gradeIdx)))
            {
                skippedRows++;
                continue;
            }

            string name = parts[nameIdx].Trim();
            string subjectName = parts[subjectIdx].Trim();
            if (!double.TryParse(parts[gradeIdx].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double gradeVal))
            {
                skippedRows++;
                continue;
            }

            if (!studentsDict.TryGetValue(name, out Student? student))
            {
                student = new Student(name);
                studentsDict[name] = student;
            }

            Subject? subject = student.Subjects.FirstOrDefault(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
            if (subject == null)
            {
                subject = new Subject(subjectName);
                student.Subjects.Add(subject);
            }
            subject.Grades.Add(new Grade(gradeVal));
        }

        if (skippedRows > 0)
        {
            MessageBox.Show($"Import completed. Skipped {skippedRows} invalid row(s).",
                "Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        return studentsDict.Values.ToList();
    }

    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var current = new StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        result.Add(current.ToString().Trim());
        return result.ToArray();
    }
}