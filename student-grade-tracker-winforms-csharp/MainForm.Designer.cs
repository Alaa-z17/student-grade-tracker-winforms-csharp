namespace StudentGradeTracker;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private MenuStrip menuStrip;
    private ToolStripMenuItem fileMenu, editMenu, viewMenu;
    private ToolStripMenuItem importItem, exportItem, exitItem;
    private ToolStripMenuItem addStudentItem;
    private ToolStripMenuItem colorItem, fontItem;
    private ToolStripMenuItem refreshItem;
    private ToolStripMenuItem autoSaveToggleItem;
    private TabControl tabControl;
    private TabPage tabGrades, tabBreakdown, tabReports;
    private ListView listViewStudents;
    private TreeView treeViewSubjects;
    private RichTextBox richTextBoxReport;
    private ProgressBar progressBarClassAvg;
    private Label lblClassAverage;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabel;
    private Panel topPanel;
    private Label titleLabel;
    private Panel bottomPanel;
    private TableLayoutPanel mainLayout;
    private ImageList iconList;
    internal Label autoSaveLabel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        iconList = new ImageList(components);
        mainLayout = new TableLayoutPanel();
        topPanel = new Panel();
        titleLabel = new Label();
        tabControl = new TabControl();
        tabGrades = new TabPage();
        listViewStudents = new ListView();
        tabBreakdown = new TabPage();
        treeViewSubjects = new TreeView();
        tabReports = new TabPage();
        richTextBoxReport = new RichTextBox();
        bottomPanel = new Panel();
        lblClassAverage = new Label();
        progressBarClassAvg = new ProgressBar();
        autoSaveLabel = new Label();
        menuStrip = new MenuStrip();
        fileMenu = new ToolStripMenuItem();
        importItem = new ToolStripMenuItem();
        exportItem = new ToolStripMenuItem();
        exitItem = new ToolStripMenuItem();
        editMenu = new ToolStripMenuItem();
        addStudentItem = new ToolStripMenuItem();
        colorItem = new ToolStripMenuItem();
        viewMenu = new ToolStripMenuItem();
        fontItem = new ToolStripMenuItem();
        refreshItem = new ToolStripMenuItem();
        autoSaveToggleItem = new ToolStripMenuItem();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();

        mainLayout.SuspendLayout();
        topPanel.SuspendLayout();
        tabControl.SuspendLayout();
        tabGrades.SuspendLayout();
        tabBreakdown.SuspendLayout();
        tabReports.SuspendLayout();
        bottomPanel.SuspendLayout();
        menuStrip.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // iconList
        iconList.ColorDepth = ColorDepth.Depth32Bit;
        iconList.ImageSize = new Size(24, 24);
        iconList.TransparentColor = Color.Transparent;

        // mainLayout
        mainLayout.ColumnCount = 1;
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        mainLayout.Controls.Add(topPanel, 0, 0);
        mainLayout.Controls.Add(tabControl, 0, 1);
        mainLayout.Controls.Add(bottomPanel, 0, 2);
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.Location = new Point(0, 31);
        mainLayout.Name = "mainLayout";
        mainLayout.RowCount = 3;
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        mainLayout.Size = new Size(950, 593);
        mainLayout.TabIndex = 0;

        // topPanel
        topPanel.BackColor = Color.FromArgb(52, 73, 94);
        topPanel.Controls.Add(titleLabel);
        topPanel.Dock = DockStyle.Fill;
        topPanel.Location = new Point(3, 3);
        topPanel.Name = "topPanel";
        topPanel.Size = new Size(944, 44);
        topPanel.TabIndex = 0;

        // titleLabel
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Location = new Point(0, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Padding = new Padding(15, 0, 0, 0);
        titleLabel.Size = new Size(944, 44);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "📚 Student Grade Tracker";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;

        // tabControl
        tabControl.Controls.Add(tabGrades);
        tabControl.Controls.Add(tabBreakdown);
        tabControl.Controls.Add(tabReports);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10F);
        tabControl.ItemSize = new Size(150, 35);
        tabControl.Location = new Point(3, 53);
        tabControl.Name = "tabControl";
        tabControl.Padding = new Point(12, 5);
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(944, 497);
        tabControl.SizeMode = TabSizeMode.Fixed;
        tabControl.TabIndex = 1;

        // tabGrades
        tabGrades.Controls.Add(listViewStudents);
        tabGrades.Location = new Point(4, 39);
        tabGrades.Name = "tabGrades";
        tabGrades.Size = new Size(936, 454);
        tabGrades.TabIndex = 0;
        tabGrades.Text = "📖 Students & Grades";

        // listViewStudents
        listViewStudents.BackColor = Color.FromArgb(240, 240, 240);
        listViewStudents.Dock = DockStyle.Fill;
        listViewStudents.Font = new Font("Segoe UI", 10F);
        listViewStudents.ForeColor = Color.FromArgb(44, 62, 80);
        listViewStudents.FullRowSelect = true;
        listViewStudents.GridLines = true;
        listViewStudents.Location = new Point(0, 0);
        listViewStudents.MultiSelect = false;
        listViewStudents.Name = "listViewStudents";
        listViewStudents.Size = new Size(936, 454);
        listViewStudents.TabIndex = 0;
        listViewStudents.UseCompatibleStateImageBehavior = false;
        listViewStudents.View = View.Details;
        listViewStudents.Columns.Add("ID", 50, HorizontalAlignment.Left);
        listViewStudents.Columns.Add("Name", 200, HorizontalAlignment.Left);
        listViewStudents.Columns.Add("Avg", 80, HorizontalAlignment.Right);
        listViewStudents.Columns.Add("Grade", 70, HorizontalAlignment.Center);

        // tabBreakdown
        tabBreakdown.Controls.Add(treeViewSubjects);
        tabBreakdown.Location = new Point(4, 39);
        tabBreakdown.Name = "tabBreakdown";
        tabBreakdown.Size = new Size(936, 454);
        tabBreakdown.TabIndex = 1;
        tabBreakdown.Text = "📊 Subject Breakdown";

        // treeViewSubjects
        treeViewSubjects.BackColor = Color.FromArgb(240, 240, 240);
        treeViewSubjects.BorderStyle = BorderStyle.None;
        treeViewSubjects.Dock = DockStyle.Fill;
        treeViewSubjects.Font = new Font("Segoe UI", 10F);
        treeViewSubjects.Location = new Point(0, 0);
        treeViewSubjects.Name = "treeViewSubjects";
        treeViewSubjects.Size = new Size(936, 454);
        treeViewSubjects.TabIndex = 0;

        // tabReports
        tabReports.Controls.Add(richTextBoxReport);
        tabReports.Location = new Point(4, 39);
        tabReports.Name = "tabReports";
        tabReports.Size = new Size(936, 454);
        tabReports.TabIndex = 2;
        tabReports.Text = "📄 Reports";

        // richTextBoxReport
        richTextBoxReport.BackColor = Color.White;
        richTextBoxReport.BorderStyle = BorderStyle.FixedSingle;
        richTextBoxReport.Dock = DockStyle.Fill;
        richTextBoxReport.Font = new Font("Consolas", 10F);
        richTextBoxReport.Location = new Point(0, 0);
        richTextBoxReport.Name = "richTextBoxReport";
        richTextBoxReport.Size = new Size(936, 454);
        richTextBoxReport.TabIndex = 0;
        richTextBoxReport.Text = "";

        // bottomPanel
        bottomPanel.BackColor = Color.FromArgb(44, 62, 80);
        bottomPanel.Controls.Add(lblClassAverage);
        bottomPanel.Controls.Add(progressBarClassAvg);
        bottomPanel.Controls.Add(autoSaveLabel);
        bottomPanel.Dock = DockStyle.Fill;
        bottomPanel.Location = new Point(3, 556);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(944, 34);
        bottomPanel.TabIndex = 2;

        // lblClassAverage
        lblClassAverage.AutoSize = true;
        lblClassAverage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblClassAverage.ForeColor = Color.White;
        lblClassAverage.Location = new Point(15, 8);
        lblClassAverage.Name = "lblClassAverage";
        lblClassAverage.Size = new Size(165, 23);
        lblClassAverage.TabIndex = 0;
        lblClassAverage.Text = "Class Average: 0.00";

        // progressBarClassAvg
        progressBarClassAvg.ForeColor = Color.FromArgb(46, 204, 113);
        progressBarClassAvg.Location = new Point(150, 7);
        progressBarClassAvg.Name = "progressBarClassAvg";
        progressBarClassAvg.Size = new Size(200, 20);
        progressBarClassAvg.Style = ProgressBarStyle.Continuous;
        progressBarClassAvg.TabIndex = 1;

        // autoSaveLabel
        autoSaveLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        autoSaveLabel.AutoSize = true;
        autoSaveLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        autoSaveLabel.ForeColor = Color.White;
        autoSaveLabel.Location = new Point(800, 10);
        autoSaveLabel.Name = "autoSaveLabel";
        autoSaveLabel.Size = new Size(127, 20);
        autoSaveLabel.TabIndex = 2;
        autoSaveLabel.Text = "Auto-save enabled";

        // menuStrip
        menuStrip.BackColor = Color.FromArgb(52, 73, 94);
        menuStrip.Font = new Font("Segoe UI", 10F);
        menuStrip.ForeColor = Color.White;
        menuStrip.ImageScalingSize = new Size(20, 20);
        menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, viewMenu });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Size = new Size(950, 31);
        menuStrip.TabIndex = 1;

        // fileMenu
        fileMenu.DropDownItems.AddRange(new ToolStripItem[] { importItem, exportItem, exitItem });
        fileMenu.Name = "fileMenu";
        fileMenu.Size = new Size(49, 27);
        fileMenu.Text = "File";

        importItem.Name = "importItem";
        importItem.Size = new Size(187, 28);
        importItem.Text = " Import CSV";

        exportItem.Name = "exportItem";
        exportItem.Size = new Size(187, 28);
        exportItem.Text = " Export CSV";

        exitItem.Name = "exitItem";
        exitItem.Size = new Size(187, 28);
        exitItem.Text = "Exit";

        // editMenu
        editMenu.DropDownItems.AddRange(new ToolStripItem[] { addStudentItem, colorItem });
        editMenu.Name = "editMenu";
        editMenu.Size = new Size(53, 27);
        editMenu.Text = "Edit";

        addStudentItem.Name = "addStudentItem";
        addStudentItem.Size = new Size(224, 28);
        addStudentItem.Text = " ➕ Add Student";

        colorItem.Name = "colorItem";
        colorItem.Size = new Size(224, 28);
        colorItem.Text = " Grade Colors";

        // viewMenu
        viewMenu.DropDownItems.AddRange(new ToolStripItem[] { fontItem, new ToolStripSeparator(), refreshItem, autoSaveToggleItem });
        viewMenu.Name = "viewMenu";
        viewMenu.Size = new Size(60, 27);
        viewMenu.Text = "View";

        fontItem.Name = "fontItem";
        fontItem.Size = new Size(189, 28);
        fontItem.Text = " Report Font";

        refreshItem.Name = "refreshItem";
        refreshItem.Size = new Size(189, 28);
        refreshItem.Text = " 🔄 Refresh";
        refreshItem.ShortcutKeyDisplayString = "F5";

        autoSaveToggleItem.Name = "autoSaveToggleItem";
        autoSaveToggleItem.Size = new Size(189, 28);
        autoSaveToggleItem.Text = " ⏹️ Auto-Save";
        autoSaveToggleItem.CheckOnClick = true;
        autoSaveToggleItem.Checked = true;

        // statusStrip
        statusStrip.BackColor = Color.FromArgb(52, 73, 94);
        statusStrip.ForeColor = Color.White;
        statusStrip.ImageScalingSize = new Size(20, 20);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 624);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(950, 26);
        statusStrip.TabIndex = 2;

        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(935, 20);
        statusLabel.Spring = true;
        statusLabel.Text = "Ready";
        statusLabel.TextAlign = ContentAlignment.MiddleLeft;

        // MainForm
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(236, 240, 241);
        ClientSize = new Size(950, 650);
        Controls.Add(mainLayout);
        Controls.Add(menuStrip);
        Controls.Add(statusStrip);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Student Grade Tracker";

        mainLayout.ResumeLayout(false);
        topPanel.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        tabGrades.ResumeLayout(false);
        tabBreakdown.ResumeLayout(false);
        tabReports.ResumeLayout(false);
        bottomPanel.ResumeLayout(false);
        bottomPanel.PerformLayout();
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}