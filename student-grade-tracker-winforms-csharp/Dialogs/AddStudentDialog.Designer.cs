namespace StudentGradeTracker.Dialogs
{
    partial class AddStudentDialog
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtSubject;
        private NumericUpDown numericGrade;
        private Button btnOK;
        private Button btnCancel;
        private ErrorProvider errorProvider1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtName = new TextBox();
            txtSubject = new TextBox();
            numericGrade = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)numericGrade).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();

            // txtName
            txtName.Location = new Point(20, 20);
            txtName.Size = new Size(260, 27);
            txtName.PlaceholderText = "Student name";
            txtName.Font = new Font("Segoe UI", 10F);

            // txtSubject
            txtSubject.Location = new Point(20, 70);
            txtSubject.Size = new Size(260, 27);
            txtSubject.PlaceholderText = "Subject name";
            txtSubject.Font = new Font("Segoe UI", 10F);

            // numericGrade
            numericGrade.Location = new Point(20, 120);
            numericGrade.Size = new Size(120, 27);
            numericGrade.Minimum = 0;
            numericGrade.Maximum = 100;
            numericGrade.Value = 50;
            numericGrade.Increment = 1;
            numericGrade.Font = new Font("Segoe UI", 10F);
            numericGrade.TextAlign = HorizontalAlignment.Center;

            // Label for grade
            Label lblGrade = new Label();
            lblGrade.Text = "Grade:";
            lblGrade.Location = new Point(20, 100);
            lblGrade.Size = new Size(50, 23);
            lblGrade.Font = new Font("Segoe UI", 10F);
            Controls.Add(lblGrade);

            // btnOK
            btnOK.Location = new Point(20, 170);
            btnOK.Size = new Size(100, 35);
            btnOK.Text = "OK";
            btnOK.Font = new Font("Segoe UI", 10F);
            btnOK.Click += btnOK_Click;

            // btnCancel
            btnCancel.Location = new Point(180, 170);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Text = "Cancel";
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Click += btnCancel_Click;

            // errorProvider1
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // AddStudentDialog
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 230);
            Controls.Add(txtName);
            Controls.Add(txtSubject);
            Controls.Add(numericGrade);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Text = "Add New Student";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ((System.ComponentModel.ISupportInitialize)numericGrade).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}