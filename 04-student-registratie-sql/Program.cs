using StudentRegistratie.Data;
using StudentRegistratie.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Ensure database is created with seed data
        using (var db = new AppDbContext())
            db.Database.EnsureCreated();

        Application.Run(new MainForm());
    }
}
