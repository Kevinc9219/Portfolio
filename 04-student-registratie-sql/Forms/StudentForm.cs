using StudentRegistratie.Data;
using StudentRegistratie.Models;

namespace StudentRegistratie.Forms;

public class StudentForm : Form
{
    private readonly Student? _existing;

    private TextBox txtFirstName = new();
    private TextBox txtLastName = new();
    private TextBox txtEmail = new();
    private DateTimePicker dtpEnrollment = new();
    private Button btnSave = new();
    private Button btnCancel = new();

    public StudentForm(Student? student = null)
    {
        _existing = student;
        InitializeLayout();
        if (student != null) PopulateFields(student);
    }

    private void InitializeLayout()
    {
        Text = _existing == null ? "Student toevoegen" : "Student bewerken";
        Size = new Size(380, 280);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 2,
            RowCount = 6,
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

        void AddRow(int row, string label, Control ctrl)
        {
            table.Controls.Add(new Label { Text = label, Anchor = AnchorStyles.Right | AnchorStyles.Top, TextAlign = ContentAlignment.MiddleRight }, 0, row);
            ctrl.Dock = DockStyle.Fill;
            table.Controls.Add(ctrl, 1, row);
        }

        txtFirstName.MaxLength = 100;
        txtLastName.MaxLength = 100;
        txtEmail.MaxLength = 200;
        dtpEnrollment.Format = DateTimePickerFormat.Short;
        dtpEnrollment.Value = DateTime.Today;

        AddRow(0, "Voornaam:", txtFirstName);
        AddRow(1, "Achternaam:", txtLastName);
        AddRow(2, "E-mail:", txtEmail);
        AddRow(3, "Inschrijfdatum:", dtpEnrollment);

        btnSave.Text = "Opslaan";
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
        table.Controls.Add(btnPanel, 0, 5);

        Controls.Add(table);
        AcceptButton = btnSave;
        CancelButton = btnCancel;
    }

    private void PopulateFields(Student s)
    {
        txtFirstName.Text = s.FirstName;
        txtLastName.Text = s.LastName;
        txtEmail.Text = s.Email;
        dtpEnrollment.Value = s.EnrollmentDate;
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        { ShowError("Voornaam is verplicht."); return; }
        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        { ShowError("Achternaam is verplicht."); return; }
        if (string.IsNullOrWhiteSpace(txtEmail.Text))
        { ShowError("E-mailadres is verplicht."); return; }
        if (!IsValidEmail(txtEmail.Text.Trim()))
        { ShowError("Voer een geldig e-mailadres in."); return; }

        using var db = new AppDbContext();

        // Unique e-mail check
        var existingId = _existing?.Id ?? 0;
        bool emailTaken = db.Students.Any(s => s.Email == txtEmail.Text.Trim() && s.Id != existingId);
        if (emailTaken)
        { ShowError("Dit e-mailadres is al in gebruik."); return; }

        if (_existing == null)
        {
            db.Students.Add(new Student
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                EnrollmentDate = dtpEnrollment.Value
            });
        }
        else
        {
            var s = db.Students.Find(_existing.Id)!;
            s.FirstName = txtFirstName.Text.Trim();
            s.LastName = txtLastName.Text.Trim();
            s.Email = txtEmail.Text.Trim();
            s.EnrollmentDate = dtpEnrollment.Value;
        }

        db.SaveChanges();
        DialogResult = DialogResult.OK;
        Close();
    }

    private static bool IsValidEmail(string email)
    {
        try { var _ = new System.Net.Mail.MailAddress(email); return true; }
        catch { return false; }
    }

    private void ShowError(string msg) =>
        MessageBox.Show(msg, "Validatiefout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
