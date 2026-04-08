namespace StudentGradeTracker.Models;

public abstract class BasePerson
{
    public string Name { get; set; }
    protected BasePerson(string name) => Name = name;
    public abstract string GetInfo();
}