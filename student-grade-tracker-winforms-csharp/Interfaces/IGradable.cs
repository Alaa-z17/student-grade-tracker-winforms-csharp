namespace StudentGradeTracker.Interfaces;

public interface IGradable
{
    double OverallAverage { get; }
    string GetGradeSummary();
}