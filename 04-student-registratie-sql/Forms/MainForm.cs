using Microsoft.EntityFrameworkCore;
using StudentRegistratie.Data;
using StudentRegistratie.Models;

namespace StudentRegistratie.Forms;

public class MainForm : Form
{
    // ── Tab control ──────────────────────────────────────────────────────────
    private TabControl tabMain = new();
    private TabPage tabStudents = new() { Text = "Studenten" };
    private TabPage tabCourses  = new() { Text = "Cursussen" };
    private TabPage tabOverview = new() { Text = "Overzicht" };
    private TabPage tabStats    = new() { Text = "Statistieken" };

    // ── Students tab ─────────────────────────────────────────────────────────
    private DataGridView dgvStudents = new();
    private TextBox txtSearch = new();
    private Button btnAddStudent = new();
    private Button btnEditStudent = new();
    private Button btnDeleteStudent = new();
    private Button btnEnroll = new();

    // ── Courses tab ──────────────────────────────────────────────────────────
    private DataGridView dgvCourses = new();
    private Button btnAddCourse = new();
    private Button btnEditCourse = new();
    private Button btnDeleteCourse = new();

    // ── Course add/edit inline controls ──────────────────────────────────────
    private Panel courseEditPanel = new();
    private TextBox txtCourseName = new();
    private TextBox txtCourseDesc = new();
    private NumericUpDown nudCredits = new();
    private Button btnSaveCourse = new();
    private Button btnCancelCourse = new();
    private int? _editingCourseId = null;

    // ── Overview tab ─────────────────────────────────────────────────────────
    private DataGridView dgvOverview = new();
    private Button btnSetGrade = new();
    private Button btnRemoveEnrollment = new();

    // ── Stats tab ────────────────────────────────────────────────────────────
    private Label lblTotalStudents = new();
    private DataGridView dgvStats = new();

    public MainForm()
    {
        Text = "Student Registratiesysteem";
        Size = new Size(900, 600);
        MinimumSize = new Size(800, 500);
        StartPosition = FormStartPosition.CenterScreen;

        BuildStudentsTab();
        BuildCoursesTab();
        BuildOverviewTab();
        BuildStatsTab();

        tabMain.TabPages.AddRange(new[] { tabStudents, tabCourses, tabOverview, tabStats });
        tabMain.Dock = DockStyle.Fill;
        tabMain.SelectedIndexChanged += (_, _) => RefreshCurrentTab();
        Controls.Add(tabMain);

        LoadStudents();
    }

    // =========================================================================
    // STUDENTS TAB
    // =========================================================================
    private void BuildStudentsTab()
    {
        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 38, Padding = new Padding(4, 4, 4, 0) };

        txtSearch.PlaceholderText = "Zoeken op naam…";
        txtSearch.Width = 180;
        txtSearch.TextChanged += (_, _) => LoadStudents();

        btnAddStudent.Text    = "➕ Toevoegen";
        btnEditStudent.Text   = "✏ Bewerken";
        btnDeleteStudent.Text = "🗑 Verwijderen";
        btnEnroll.Text        = "📋 Inschrijven";

        btnAddStudent.Click    += BtnAddStudent_Click;
        btnEditStudent.Click   += BtnEditStudent_Click;
        btnDeleteStudent.Click += BtnDeleteStudent_Click;
        btnEnroll.Click        += BtnEnroll_Click;

        toolbar.Controls.AddRange(new Control[] { txtSearch, btnAddStudent, btnEditStudent, btnDeleteStudent, btnEnroll });

        dgvStudents.Dock = DockStyle.Fill;
        dgvStudents.ReadOnly = true;
        dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvStudents.MultiSelect = false;
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvStudents.AllowUserToAddRows = false;
        dgvStudents.DoubleClick += BtnEditStudent_Click;

        tabStudents.Controls.Add(dgvStudents);
        tabStudents.Controls.Add(toolbar);
    }

    private void LoadStudents()
    {
        using var db = new AppDbContext();
        var query = db.Students.AsQueryable();

        var search = txtSearch.Text.Trim();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(s => (s.FirstName + " " + s.LastName).Contains(search)
                                  || s.LastName.Contains(search)
                                  || s.FirstName.Contains(search));

        var list = query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(s => new
            {
                s.Id,
                Voornaam = s.FirstName,
                Achternaam = s.LastName,
                Email = s.Email,
                Inschrijfdatum = s.EnrollmentDate.ToString("dd-MM-yyyy"),
                Cursussen = s.Enrollments.Count()
            }).ToList();

        dgvStudents.DataSource = list;
        if (dgvStudents.Columns.Contains("Id"))
            dgvStudents.Columns["Id"]!.Visible = false;
    }

    private void BtnAddStudent_Click(object? sender, EventArgs e)
    {
        using var form = new StudentForm();
        if (form.ShowDialog(this) == DialogResult.OK) LoadStudents();
    }

    private void BtnEditStudent_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvStudents);
        if (id == null) return;
        using var db = new AppDbContext();
        var student = db.Students.Find(id.Value);
        if (student == null) return;
        using var form = new StudentForm(student);
        if (form.ShowDialog(this) == DialogResult.OK) LoadStudents();
    }

    private void BtnDeleteStudent_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvStudents);
        if (id == null) return;

        var confirm = MessageBox.Show(
            "Student en alle inschrijvingen verwijderen?",
            "Bevestigen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm != DialogResult.Yes) return;

        using var db = new AppDbContext();
        var student = db.Students.Find(id.Value);
        if (student == null) return;
        db.Students.Remove(student);
        db.SaveChanges();
        LoadStudents();
    }

    private void BtnEnroll_Click(object? sender, EventArgs e)
    {
        using var form = new EnrollmentForm();
        if (form.ShowDialog(this) == DialogResult.OK) LoadStudents();
    }

    // =========================================================================
    // COURSES TAB
    // =========================================================================
    private void BuildCoursesTab()
    {
        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 38, Padding = new Padding(4, 4, 4, 0) };

        btnAddCourse.Text    = "➕ Toevoegen";
        btnEditCourse.Text   = "✏ Bewerken";
        btnDeleteCourse.Text = "🗑 Verwijderen";

        btnAddCourse.Click    += BtnAddCourse_Click;
        btnEditCourse.Click   += BtnEditCourse_Click;
        btnDeleteCourse.Click += BtnDeleteCourse_Click;

        toolbar.Controls.AddRange(new Control[] { btnAddCourse, btnEditCourse, btnDeleteCourse });

        dgvCourses.Dock = DockStyle.Fill;
        dgvCourses.ReadOnly = true;
        dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCourses.MultiSelect = false;
        dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCourses.AllowUserToAddRows = false;
        dgvCourses.DoubleClick += BtnEditCourse_Click;

        // Inline edit panel
        courseEditPanel.Dock = DockStyle.Bottom;
        courseEditPanel.Height = 130;
        courseEditPanel.Padding = new Padding(8);
        courseEditPanel.Visible = false;
        courseEditPanel.BorderStyle = BorderStyle.FixedSingle;

        nudCredits.Minimum = 1; nudCredits.Maximum = 12; nudCredits.Value = 3;

        var fl = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        fl.Controls.Add(MakeLabeled("Naam:", txtCourseName));
        fl.Controls.Add(MakeLabeled("Omschrijving:", txtCourseDesc));
        fl.Controls.Add(MakeLabeled("Credits:", nudCredits));

        btnSaveCourse.Text   = "Opslaan";
        btnCancelCourse.Text = "Annuleren";
        btnSaveCourse.Click   += BtnSaveCourse_Click;
        btnCancelCourse.Click += (_, _) => HideCoursePanel();

        var btnRow = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 30 };
        btnRow.Controls.AddRange(new Control[] { btnSaveCourse, btnCancelCourse });

        courseEditPanel.Controls.Add(fl);
        courseEditPanel.Controls.Add(btnRow);

        tabCourses.Controls.Add(dgvCourses);
        tabCourses.Controls.Add(courseEditPanel);
        tabCourses.Controls.Add(toolbar);
    }

    private static Panel MakeLabeled(string label, Control ctrl)
    {
        var p = new Panel { Width = 180, Height = 90 };
        p.Controls.Add(new Label { Text = label, Dock = DockStyle.Top });
        ctrl.Dock = DockStyle.Top;
        p.Controls.Add(ctrl);
        return p;
    }

    private void LoadCourses()
    {
        using var db = new AppDbContext();
        var list = db.Courses.Select(c => new
        {
            c.Id,
            Naam = c.Name,
            Omschrijving = c.Description,
            Credits = c.Credits,
            Studenten = c.Enrollments.Count()
        }).OrderBy(c => c.Naam).ToList();

        dgvCourses.DataSource = list;
        if (dgvCourses.Columns.Contains("Id"))
            dgvCourses.Columns["Id"]!.Visible = false;
    }

    private void BtnAddCourse_Click(object? sender, EventArgs e)
    {
        _editingCourseId = null;
        txtCourseName.Clear(); txtCourseDesc.Clear(); nudCredits.Value = 3;
        courseEditPanel.Visible = true;
        txtCourseName.Focus();
    }

    private void BtnEditCourse_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvCourses);
        if (id == null) return;
        using var db = new AppDbContext();
        var c = db.Courses.Find(id.Value);
        if (c == null) return;
        _editingCourseId = c.Id;
        txtCourseName.Text = c.Name;
        txtCourseDesc.Text = c.Description;
        nudCredits.Value = c.Credits;
        courseEditPanel.Visible = true;
        txtCourseName.Focus();
    }

    private void BtnSaveCourse_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCourseName.Text))
        { MessageBox.Show("Naam is verplicht.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

        using var db = new AppDbContext();
        if (_editingCourseId == null)
        {
            db.Courses.Add(new Course
            {
                Name = txtCourseName.Text.Trim(),
                Description = txtCourseDesc.Text.Trim(),
                Credits = (int)nudCredits.Value
            });
        }
        else
        {
            var c = db.Courses.Find(_editingCourseId.Value)!;
            c.Name = txtCourseName.Text.Trim();
            c.Description = txtCourseDesc.Text.Trim();
            c.Credits = (int)nudCredits.Value;
        }
        db.SaveChanges();
        HideCoursePanel();
        LoadCourses();
    }

    private void BtnDeleteCourse_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvCourses);
        if (id == null) return;
        var confirm = MessageBox.Show(
            "Cursus en alle inschrijvingen verwijderen?",
            "Bevestigen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm != DialogResult.Yes) return;
        using var db = new AppDbContext();
        var c = db.Courses.Find(id.Value);
        if (c == null) return;
        db.Courses.Remove(c);
        db.SaveChanges();
        LoadCourses();
    }

    private void HideCoursePanel()
    {
        courseEditPanel.Visible = false;
        _editingCourseId = null;
    }

    // =========================================================================
    // OVERVIEW TAB
    // =========================================================================
    private void BuildOverviewTab()
    {
        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 38, Padding = new Padding(4, 4, 4, 0) };
        btnSetGrade.Text = "🖊 Cijfer instellen";
        btnRemoveEnrollment.Text = "❌ Inschrijving verwijderen";
        btnSetGrade.Click += BtnSetGrade_Click;
        btnRemoveEnrollment.Click += BtnRemoveEnrollment_Click;
        toolbar.Controls.AddRange(new Control[] { btnSetGrade, btnRemoveEnrollment });

        dgvOverview.Dock = DockStyle.Fill;
        dgvOverview.ReadOnly = true;
        dgvOverview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOverview.MultiSelect = false;
        dgvOverview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOverview.AllowUserToAddRows = false;

        tabOverview.Controls.Add(dgvOverview);
        tabOverview.Controls.Add(toolbar);
    }

    private void LoadOverview()
    {
        using var db = new AppDbContext();
        var list = db.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .OrderBy(e => e.Student.LastName)
            .ThenBy(e => e.Course.Name)
            .Select(e => new
            {
                e.Id,
                Student = e.Student.FirstName + " " + e.Student.LastName,
                Cursus = e.Course.Name,
                Credits = e.Course.Credits,
                Cijfer = e.Grade.HasValue ? e.Grade.Value.ToString("F1") : "—"
            }).ToList();

        dgvOverview.DataSource = list;
        if (dgvOverview.Columns.Contains("Id"))
            dgvOverview.Columns["Id"]!.Visible = false;
    }

    private void BtnSetGrade_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvOverview);
        if (id == null) return;

        using var db = new AppDbContext();
        var enrollment = db.Enrollments.Find(id.Value);
        if (enrollment == null) return;

        var input = ShowInputDialog("Cijfer instellen", "Voer een cijfer in (1.0 – 10.0):",
            enrollment.Grade?.ToString("F1") ?? "5.5");

        if (input == null) return;
        if (!double.TryParse(input.Replace(',', '.'),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var grade)
            || grade < 1.0 || grade > 10.0)
        {
            MessageBox.Show("Geldig cijfer tussen 1.0 en 10.0 vereist.", "Ongeldige invoer",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        enrollment.Grade = grade;
        db.SaveChanges();
        LoadOverview();
    }

    private void BtnRemoveEnrollment_Click(object? sender, EventArgs e)
    {
        var id = GetSelectedId(dgvOverview);
        if (id == null) return;
        var confirm = MessageBox.Show("Inschrijving verwijderen?", "Bevestigen",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm != DialogResult.Yes) return;
        using var db = new AppDbContext();
        var en = db.Enrollments.Find(id.Value);
        if (en == null) return;
        db.Enrollments.Remove(en);
        db.SaveChanges();
        LoadOverview();
    }

    // =========================================================================
    // STATS TAB
    // =========================================================================
    private void BuildStatsTab()
    {
        var panel = new Panel { Dock = DockStyle.Fill };

        lblTotalStudents.Dock = DockStyle.Top;
        lblTotalStudents.Font = new Font(Font.FontFamily, 11, FontStyle.Bold);
        lblTotalStudents.Padding = new Padding(8, 8, 0, 4);
        lblTotalStudents.Height = 36;

        var header = new Label
        {
            Text = "Gemiddeld cijfer per cursus:",
            Dock = DockStyle.Top,
            Padding = new Padding(8, 0, 0, 0),
            Height = 22,
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };

        dgvStats.Dock = DockStyle.Fill;
        dgvStats.ReadOnly = true;
        dgvStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvStats.AllowUserToAddRows = false;
        dgvStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        panel.Controls.Add(dgvStats);
        panel.Controls.Add(header);
        panel.Controls.Add(lblTotalStudents);
        tabStats.Controls.Add(panel);
    }

    private void LoadStats()
    {
        using var db = new AppDbContext();
        lblTotalStudents.Text = $"Totaal aantal studenten: {db.Students.Count()}";

        var stats = db.Courses
            .Select(c => new
            {
                Cursus = c.Name,
                Credits = c.Credits,
                Inschrijvingen = c.Enrollments.Count(),
                GemiddeldCijfer = c.Enrollments.Where(e => e.Grade != null).Any()
                    ? c.Enrollments.Where(e => e.Grade != null).Average(e => e.Grade)
                    : (double?)null
            })
            .OrderBy(c => c.Cursus)
            .ToList()
            .Select(c => new
            {
                c.Cursus,
                c.Credits,
                c.Inschrijvingen,
                GemiddeldCijfer = c.GemiddeldCijfer.HasValue ? c.GemiddeldCijfer.Value.ToString("F2") : "—"
            }).ToList();

        dgvStats.DataSource = stats;
    }

    // =========================================================================
    // HELPERS
    // =========================================================================
    private void RefreshCurrentTab()
    {
        switch (tabMain.SelectedTab?.Text)
        {
            case "Studenten":   LoadStudents();  break;
            case "Cursussen":   LoadCourses();   break;
            case "Overzicht":   LoadOverview();  break;
            case "Statistieken": LoadStats();    break;
        }
    }

    private static int? GetSelectedId(DataGridView dgv)
    {
        if (dgv.CurrentRow == null) return null;
        var cell = dgv.CurrentRow.Cells["Id"];
        return cell?.Value is int id ? id : null;
    }

    private static string? ShowInputDialog(string title, string prompt, string defaultValue = "")
    {
        var form = new Form
        {
            Text = title, Size = new Size(320, 140),
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false, MinimizeBox = false
        };
        var lbl = new Label { Text = prompt, Left = 10, Top = 12, Width = 290 };
        var txt = new TextBox { Text = defaultValue, Left = 10, Top = 36, Width = 285 };
        var ok  = new Button { Text = "OK",       Left = 130, Top = 68, Width = 75, DialogResult = DialogResult.OK };
        var ca  = new Button { Text = "Annuleren",Left = 215, Top = 68, Width = 85, DialogResult = DialogResult.Cancel };
        form.Controls.AddRange(new Control[] { lbl, txt, ok, ca });
        form.AcceptButton = ok; form.CancelButton = ca;
        txt.SelectAll();
        return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
    }
}
