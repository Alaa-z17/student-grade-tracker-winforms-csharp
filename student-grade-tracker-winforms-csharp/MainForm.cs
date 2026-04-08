using StudentGradeTracker.Dialogs;
using StudentGradeTracker.Models;
using StudentGradeTracker.Reports;
using StudentGradeTracker.Services;
using System.Text;

namespace StudentGradeTracker;

public partial class MainForm : Form
{
    private List<Student> students = new();
    private List<Student> filteredStudents = new();
    private System.Windows.Forms.Timer? autoSaveTimer;
    private NotifyIcon? notifyIcon;
    private ContextMenuStrip? contextMenu;
    private bool autoSaveEnabled = true;
    private string currentFilter = "";
    private int sortColumn = -1;
    private bool sortAscending = true;

    private Dictionary<string, Color> gradeColors = new()
    {
        {"A", Color.FromArgb(200, 230, 201)},
        {"B", Color.FromArgb(197, 217, 241)},
        {"C", Color.FromArgb(255, 242, 204)},
        {"D", Color.FromArgb(255, 224, 178)},
        {"F", Color.FromArgb(255, 199, 206)}
    };

    public MainForm()
    {
        InitializeComponent();
        AttachEventHandlers();
        LoadData();
        SetupAutoSave();
        SetupNotifyIcon();
        SetupContextMenu();
        SetupListViewSorting();
        SetupSearchBox();
        RefreshAllUI();
        UpdateStatusMessage("Ready");
    }

    private void AttachEventHandlers()
    {
        importItem.Click += importToolStripMenuItem_Click;
        exportItem.Click += exportToolStripMenuItem_Click;
        exitItem.Click += exitToolStripMenuItem_Click;
        addStudentItem.Click += addStudentToolStripMenuItem_Click;
        colorItem.Click += setGradeColorsToolStripMenuItem_Click;
        fontItem.Click += formatReportToolStripMenuItem_Click;
        listViewStudents.DoubleClick += listViewStudents_DoubleClick;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        autoSaveToggleItem.Click += autoSaveToggleItem_Click;
        refreshItem.Click += (s, e) => RefreshAllUI();
    }

    private void SetupSearchBox()
    {
        Panel searchPanel = new Panel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(5) };
        TextBox txtSearch = new TextBox { Width = 200, PlaceholderText = "Search by name...", Font = new Font("Segoe UI", 10F) };
        Button btnSearch = new Button { Text = "🔍", Width = 35, Height = 27, FlatStyle = FlatStyle.Flat };
        btnSearch.Click += (s, e) => { currentFilter = txtSearch.Text.Trim(); ApplyFilter(); };
        txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { currentFilter = txtSearch.Text.Trim(); ApplyFilter(); } };
        Button btnClear = new Button { Text = "✖", Width = 35, Height = 27, FlatStyle = FlatStyle.Flat };
        btnClear.Click += (s, e) => { txtSearch.Clear(); currentFilter = ""; ApplyFilter(); };

        searchPanel.Controls.Add(txtSearch);
        searchPanel.Controls.Add(btnSearch);
        searchPanel.Controls.Add(btnClear);
        txtSearch.Location = new Point(5, 5);
        btnSearch.Location = new Point(210, 5);
        btnClear.Location = new Point(250, 5);

        tabGrades.Controls.Add(searchPanel);
        searchPanel.BringToFront();
    }

    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(currentFilter))
            filteredStudents = new List<Student>(students);
        else
            filteredStudents = students.Where(s => s.Name.Contains(currentFilter, StringComparison.OrdinalIgnoreCase)).ToList();
        ApplySorting(); // re-sort after filter
        RefreshStudentListView();
        UpdateStatusMessage($"Filtered: {filteredStudents.Count} of {students.Count} students");
    }

    private void SetupListViewSorting()
    {
        listViewStudents.ColumnClick += (s, e) =>
        {
            if (e.Column == sortColumn)
                sortAscending = !sortAscending;
            else
            {
                sortColumn = e.Column;
                sortAscending = true;
            }
            ApplySorting();
        };
    }

    private void ApplySorting()
    {
        if (sortColumn == -1) return;
        var sorted = sortColumn switch
        {
            0 => sortAscending ? filteredStudents.OrderBy(s => s.Id).ToList() : filteredStudents.OrderByDescending(s => s.Id).ToList(),
            1 => sortAscending ? filteredStudents.OrderBy(s => s.Name).ToList() : filteredStudents.OrderByDescending(s => s.Name).ToList(),
            2 => sortAscending ? filteredStudents.OrderBy(s => s.OverallAverage).ToList() : filteredStudents.OrderByDescending(s => s.OverallAverage).ToList(),
            3 => sortAscending ? filteredStudents.OrderBy(s => s.LetterGrade).ToList() : filteredStudents.OrderByDescending(s => s.LetterGrade).ToList(),
            _ => filteredStudents
        };
        filteredStudents = sorted;
        RefreshStudentListView();
    }

    private void LoadData()
    {
        try
        {
            students = JsonStorageService.LoadStudents();
            filteredStudents = new List<Student>(students);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            students = new List<Student>();
            filteredStudents = new List<Student>();
        }
    }

    private void SetupAutoSave()
    {
        autoSaveTimer = new System.Windows.Forms.Timer();
        autoSaveTimer.Interval = 30000;
        autoSaveTimer.Tick += (s, e) =>
        {
            if (autoSaveEnabled)
            {
                SafeSaveStudents();
                notifyIcon?.ShowBalloonTip(1000, "Auto-Save", "Student data saved.", ToolTipIcon.Info);
                UpdateStatusMessage($"Auto-saved at {DateTime.Now:t}");
            }
        };
        autoSaveTimer.Start();
    }

    private void SetupNotifyIcon()
    {
        notifyIcon = new NotifyIcon();
        notifyIcon.Icon = SystemIcons.Application;
        notifyIcon.Visible = true;
        notifyIcon.Text = "Student Grade Tracker";
        notifyIcon.BalloonTipTitle = "Student Grade Tracker";
    }

    private void SetupContextMenu()
    {
        contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("✏️ Edit Grades", null, (s, e) => EditSelectedStudent());
        contextMenu.Items.Add("🗑️ Delete Student", null, (s, e) => DeleteSelectedStudent());
        listViewStudents.ContextMenuStrip = contextMenu;
    }

    private void UpdateStatusMessage(string message)
    {
        if (statusLabel != null)
            statusLabel.Text = message;
    }

    private void RefreshAllUI()
    {
        ApplyFilter(); // refreshes list and respects filter & sort
        RefreshTreeView();
        UpdateClassStatistics();
        UpdateReportsTab();
    }

    private void RefreshStudentListView()
    {
        listViewStudents.BeginUpdate();
        listViewStudents.Items.Clear();
        int index = 0;
        foreach (var s in filteredStudents)
        {
            var item = new ListViewItem(s.Id.ToString());
            item.SubItems.Add(s.Name);
            item.SubItems.Add(s.OverallAverage.ToString("F2"));
            item.SubItems.Add(s.LetterGrade);
            item.Tag = s;

            item.BackColor = (index % 2 == 0) ? Color.FromArgb(248, 248, 248) : Color.White;
            if (gradeColors.TryGetValue(s.LetterGrade, out Color gradeColor))
                item.BackColor = gradeColor;

            listViewStudents.Items.Add(item);
            index++;
        }
        listViewStudents.EndUpdate();
    }

    private void RefreshTreeView()
    {
        treeViewSubjects.Nodes.Clear();
        foreach (var s in students)
        {
            var studentNode = new TreeNode($"{s.Name}  (Avg: {s.OverallAverage:F2} - {s.LetterGrade})");
            studentNode.ForeColor = Color.FromArgb(44, 62, 80);
            studentNode.NodeFont = new Font("Segoe UI", 9, FontStyle.Bold);
            foreach (var subject in s.Subjects)
            {
                var subjectNode = new TreeNode($"{subject.Name}  [Avg: {subject.AverageGrade:F2}]");
                subjectNode.ForeColor = Color.FromArgb(52, 73, 94);
                foreach (var grade in subject.Grades)
                    subjectNode.Nodes.Add($"Grade: {grade.Value}");
                studentNode.Nodes.Add(subjectNode);
            }
            treeViewSubjects.Nodes.Add(studentNode);
        }
        treeViewSubjects.ExpandAll();
    }

    private void UpdateClassStatistics()
    {
        if (students.Count == 0)
        {
            progressBarClassAvg.Value = 0;
            lblClassAverage.Text = "Class Average: 0.00";
            return;
        }
        double classAvg = students.Average(s => s.OverallAverage);
        progressBarClassAvg.Value = (int)classAvg;
        lblClassAverage.Text = $"Class Average: {classAvg:F2}";
    }

    private void UpdateReportsTab()
    {
        if (students.Count == 0)
        {
            richTextBoxReport.Text = "No students to display.";
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("Student ID\tStudent Name\tOverall Average\tLetter Grade");
        sb.AppendLine(new string('-', 70));
        foreach (var s in students.OrderBy(s => s.Name))
        {
            sb.AppendLine($"{s.Id}\t{s.Name}\t{s.OverallAverage:F2}\t{s.LetterGrade}");
        }
        sb.AppendLine();
        sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Total Students: {students.Count}");

        richTextBoxReport.Text = sb.ToString();
        richTextBoxReport.Font = new Font("Consolas", 10);
    }

    private void EditSelectedStudent()
    {
        if (listViewStudents.SelectedItems.Count == 0)
        {
            MessageBox.Show("Please select a student first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var selected = listViewStudents.SelectedItems[0].Tag as Student;
        if (selected == null) return;
        using (var dlg = new AddGradeDialog(selected))
        {
            if (dlg.ShowDialog() == DialogResult.OK)
                RefreshAllUI();
        }
    }

    private void DeleteSelectedStudent()
    {
        if (listViewStudents.SelectedItems.Count == 0)
        {
            MessageBox.Show("Please select a student to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var selected = listViewStudents.SelectedItems[0].Tag as Student;
        if (selected == null) return;
        var result = MessageBox.Show("Delete selected student permanently?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            students.Remove(selected);
            RefreshAllUI();
            SafeSaveStudents();
            UpdateStatusMessage($"Deleted {selected.Name}");
        }
    }

    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.Title = "Import Students from CSV";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var imported = CsvImportService.ImportFromCsv(ofd.FileName);
                    students.AddRange(imported);
                    RefreshAllUI();
                    SafeSaveStudents();
                    MessageBox.Show($"Imported {imported.Count} students. Total students: {students.Count}.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateStatusMessage($"Imported {imported.Count} students from {Path.GetFileName(ofd.FileName)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Import failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void exportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (students.Count == 0)
        {
            MessageBox.Show("No students to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using (SaveFileDialog sfd = new SaveFileDialog())
        {
            sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.Title = "Export Students to CSV";
            sfd.FileName = $"students_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CsvExportService.ExportToCsv(students, sfd.FileName);
                    MessageBox.Show($"Exported {students.Count} students to {Path.GetFileName(sfd.FileName)}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateStatusMessage($"Exported to {Path.GetFileName(sfd.FileName)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void setGradeColorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (var grade in new[] { "A", "B", "C", "D", "F" })
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = gradeColors[grade];
                if (cd.ShowDialog() == DialogResult.OK)
                    gradeColors[grade] = cd.Color;
            }
        }
        RefreshStudentListView();
        UpdateStatusMessage("Grade colors updated");
    }

    private void formatReportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (FontDialog fd = new FontDialog())
        {
            fd.Font = richTextBoxReport.Font;
            fd.ShowEffects = true;
            fd.AllowScriptChange = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBoxReport.Font = fd.Font;
                UpdateStatusMessage($"Report font changed to {fd.Font.Name}, {fd.Font.Size}pt");
            }
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        autoSaveTimer?.Stop();
        SafeSaveStudents();
        notifyIcon?.Dispose();
        Application.Exit();
    }

    private void listViewStudents_DoubleClick(object? sender, EventArgs e) => EditSelectedStudent();

    private void tabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (tabControl.SelectedTab == tabReports)
            UpdateStatusMessage("Reports tab - use Format Report to change font");
        else if (tabControl.SelectedTab == tabBreakdown)
            UpdateStatusMessage("Subject breakdown view");
        else
            UpdateStatusMessage("Students & Grades view - right-click for options");
    }

    private void AddNewStudent()
    {
        using (var dialog = new AddStudentDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (students.Any(s => s.Name.Equals(dialog.StudentName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A student with that name already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newStudent = new Student(dialog.StudentName);
                var subject = new Subject(dialog.SubjectName);
                subject.Grades.Add(new Grade(dialog.GradeValue));
                newStudent.Subjects.Add(subject);

                students.Add(newStudent);
                RefreshAllUI();
                SafeSaveStudents();
                UpdateStatusMessage($"Added student: {dialog.StudentName} with {dialog.SubjectName} grade {dialog.GradeValue}");
            }
        }
    }

    private void addStudentToolStripMenuItem_Click(object sender, EventArgs e) => AddNewStudent();

    private void SafeSaveStudents()
    {
        try
        {
            JsonStorageService.SaveStudents(students);
            UpdateStatusMessage($"Saved at {DateTime.Now:t}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving data: {ex.Message}", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void autoSaveToggleItem_Click(object sender, EventArgs e)
    {
        autoSaveEnabled = autoSaveToggleItem.Checked;
        autoSaveLabel.Text = autoSaveEnabled ? "Auto-save enabled" : "Auto-save disabled";
        UpdateStatusMessage(autoSaveEnabled ? "Auto-save enabled" : "Auto-save disabled");
    }
}