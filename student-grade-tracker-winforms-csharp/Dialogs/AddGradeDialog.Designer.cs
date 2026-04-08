namespace StudentGradeTracker.Dialogs;

partial class AddGradeDialog
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtSubject;
    private NumericUpDown numericGrade;
    private Button btnSave;
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
        txtSubject = new TextBox();
        numericGrade = new NumericUpDown();
        btnSave = new Button();
        errorProvider1 = new ErrorProvider(components);
        ((System.ComponentModel.ISupportInitialize)numericGrade).BeginInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();

        txtSubject.Location = new Point(30, 30);
        txtSubject.Size = new Size(200, 27);
        txtSubject.PlaceholderText = "Subject name";
        txtSubject.Font = new Font("Segoe UI", 10F);

        Label lblGrade = new Label();
        lblGrade.Text = "Grade:";
        lblGrade.Location = new Point(30, 70);
        lblGrade.Size = new Size(50, 23);
        lblGrade.Font = new Font("Segoe UI", 10F);
        Controls.Add(lblGrade);

        numericGrade.Location = new Point(30, 95);
        numericGrade.Size = new Size(120, 27);
        numericGrade.Minimum = 0;
        numericGrade.Maximum = 100;
        numericGrade.Value = 50;
        numericGrade.Increment = 1;
        numericGrade.Font = new Font("Segoe UI", 10F);
        numericGrade.TextAlign = HorizontalAlignment.Center;

        btnSave.Location = new Point(30, 140);
        btnSave.Size = new Size(100, 35);
        btnSave.Text = "Save";
        btnSave.Font = new Font("Segoe UI", 10F);
        btnSave.Click += btnSave_Click;

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(320, 210);
        Controls.Add(txtSubject);
        Controls.Add(numericGrade);
        Controls.Add(btnSave);
        Text = "Add / Edit Grade";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        ((System.ComponentModel.ISupportInitialize)numericGrade).EndInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}