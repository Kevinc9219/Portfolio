using Microsoft.EntityFrameworkCore;
using StudentRegistratie.Data;
using StudentRegistratie.Models;

namespace StudentRegistratie.Forms;

public class EnrollmentForm : Form
{
    private ComboBox cmbStudent = new();
    private ComboBox cmbCourse = new();
    private NumericUpDown nudGrade = new();
    private CheckBox chkGrade = new();
    private Button btnSave = new();
    private Button btnCancel = new();

    public EnrollmentForm()
    {
        InitializeLayout();
        LoadData();
    }

    private void InitializeLayout()
    {
        Text = "Student inschrijven";
        Size = new Size(380, 250);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        nudGrade.Minimum = 1;
        nudGrade.Maximum = 10;
        nudGrade.DecimalPlaces = 1;
        nudGrade.Increment = 0.5m;
        nudGrade.Value = 5.5m;
        nudGrade.Enabled = false;

        chkGrade.Text = "Cijfer toekennen";
        chkGrade.CheckedChanged += (_, _) => nudGrade.Enabled = chkGrade.Checked;

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 2,
            RowCount = 5,
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));

        void AddRow(int row, string label, Control ctrl)
        {
            table.Controls.Add(new Label { Text = label, Anchor = AnchorStyles.Right | AnchorStyles.Top, TextAlign = ContentAlignment.MiddleRight }, 0, row);
            ctrl.Dock = DockStyle.Fill;
            table.Controls.Add(ctrl, 1, row);
        }

        cmbStudent.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;

        AddRow(0, "Student:", cmbStudent);
        AddRow(1, "Cursus:", cmbCourse);
        AddRow(2, string.Empty, chkGrade);
        AddRow(3, "Cijfer:", nudGrade);

        btnSave.Text = "Inschrijven";
        btnSave.Click += BtnSave_Click;
        btnCancel.Text = "Annuleren";
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

        var btnPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
        };
        btnPanel.Controls.Add(btnCancel);
        btnPanel.Controls.Add(btnSave);
        table.SetColumnSpan(btnPanel, 2);
        table.Controls.Add(btnPanel, 0, 4);

        Controls.Add(table);
        AcceptButton = btnSave;
        CancelButton = btnCancel;
    }

    private void LoadData()
    {
        using var db = new AppDbContext();

        var students = db.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        cmbStudent.DataSource = students;
        cmbStudent.DisplayMember = "FullName";
        cmbStudent.ValueMember = "Id";

        var courses = db.Courses.OrderBy(c => c.Name).ToList();
        cmbCourse.DataSource = courses;
        cmbCourse.DisplayMember = "Name";
        cmbCourse.ValueMember = "Id";
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (cmbStudent.SelectedValue is not int studentId)
        { MessageBox.Show("Kies een student.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        if (cmbCourse.SelectedValue is not int courseId)
        { MessageBox.Show("Kies een cursus.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

        using var db = new AppDbContext();

        bool alreadyEnrolled = db.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId);
        if (alreadyEnrolled)
        { MessageBox.Show("Deze student is al ingeschreven voor deze cursus.", "Al ingeschreven", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

        db.Enrollments.Add(new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            Grade = chkGrade.Checked ? (double?)nudGrade.Value : null
        });

        db.SaveChanges();
        DialogResult = DialogResult.OK;
        Close();
    }
}
