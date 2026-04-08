using StudentGradeTracker.Models;

namespace StudentGradeTracker.Dialogs;

public partial class AddGradeDialog : Form
{
    private readonly Student _student;

    public AddGradeDialog(Student student)
    {
        InitializeComponent();
        _student = student ?? throw new ArgumentNullException(nameof(student));
        numericGrade.Minimum = 0;
        numericGrade.Maximum = 100;
        numericGrade.Value = 50;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string subjectName = txtSubject.Text.Trim();
        if (string.IsNullOrEmpty(subjectName))
        {
            errorProvider1.SetError(txtSubject, "Subject name required");
            return;
        }

        double gradeValue = (double)numericGrade.Value;

        Subject? subject = _student.Subjects.FirstOrDefault(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
        if (subject == null)
        {
            subject = new Subject(subjectName);
            _student.Subjects.Add(subject);
        }

        subject.Grades.Add(new Grade(gradeValue));
        DialogResult = DialogResult.OK;
        Close();
    }
}