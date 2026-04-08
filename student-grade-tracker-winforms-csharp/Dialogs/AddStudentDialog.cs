using System;
using System.Windows.Forms;

namespace StudentGradeTracker.Dialogs
{
    public partial class AddStudentDialog : Form
    {
        public string StudentName { get; private set; } = string.Empty;
        public string SubjectName { get; private set; } = string.Empty;
        public double GradeValue { get; private set; }

        public AddStudentDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Student name is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                errorProvider1.SetError(txtSubject, "Subject name is required.");
                return;
            }

            StudentName = txtName.Text.Trim();
            SubjectName = txtSubject.Text.Trim();
            GradeValue = (double)numericGrade.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}