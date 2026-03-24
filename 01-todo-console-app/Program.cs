using TodoConsoleApp.Services;

var service = new TaskService();

while (true)
{
    Console.ResetColor();
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════╗");
    Console.WriteLine("║       TO-DO CONSOLE APP      ║");
    Console.WriteLine("╚══════════════════════════════╝");
    Console.ResetColor();

    Console.Write("  Open taken: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(service.OpenCount);
    Console.ResetColor();
    Console.Write("   |   Voltooid: ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(service.CompletedCount);
    Console.ResetColor();
    Console.WriteLine();

    Console.WriteLine("  1. Taak toevoegen");
    Console.WriteLine("  2. Alle taken weergeven");
    Console.WriteLine("  3. Taak markeren als voltooid");
    Console.WriteLine("  4. Taak verwijderen");
    Console.WriteLine("  5. Zoeken op naam");
    Console.WriteLine("  6. Afsluiten");
    Console.WriteLine();
    Console.Write("  Keuze: ");

    var input = Console.ReadLine()?.Trim();

    switch (input)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ShowTasks();
            break;
        case "3":
            CompleteTask();
            break;
        case "4":
            DeleteTask();
            break;
        case "5":
            SearchTasks();
            break;
        case "6":
            Console.ResetColor();
            Console.WriteLine("\n  Tot ziens!");
            return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  Ongeldige keuze.");
            Console.ResetColor();
            Pause();
            break;
    }
}

void AddTask()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("── Taak toevoegen ──");
    Console.ResetColor();

    string title;
    while (true)
    {
        Console.Write("  Naam (verplicht): ");
        title = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(title)) break;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  Naam mag niet leeg zijn.");
        Console.ResetColor();
    }

    Console.Write("  Beschrijving (optioneel): ");
    var description = Console.ReadLine()?.Trim();

    try
    {
        service.Add(title, string.IsNullOrEmpty(description) ? null : description);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  Taak '{title}' toegevoegd.");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n  Fout: {ex.Message}");
        Console.ResetColor();
    }

    Pause();
}

void ShowTasks()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("── Alle taken ──");
    Console.ResetColor();

    var tasks = service.GetAll();
    if (tasks.Count == 0)
    {
        Console.WriteLine("  Geen taken gevonden.");
        Pause();
        return;
    }

    Console.WriteLine();
    for (int i = 0; i < tasks.Count; i++)
    {
        var task = tasks[i];
        string status = task.IsCompleted ? "[✓]" : "[ ]";

        Console.ForegroundColor = task.IsCompleted ? ConsoleColor.Green : ConsoleColor.White;
        Console.Write($"  {i + 1,2}. {status} {task.Title}");

        if (!string.IsNullOrEmpty(task.Description))
            Console.Write($" — {task.Description}");

        Console.WriteLine($"  ({task.CreatedAt:dd-MM-yyyy})");
        Console.ResetColor();
    }

    Pause();
}

void CompleteTask()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("── Taak voltooien ──");
    Console.ResetColor();

    ShowInlineList();

    Console.Write("  Nummer: ");
    if (!int.TryParse(Console.ReadLine(), out int nr))
    {
        ShowError("Ongeldig nummer.");
        Pause();
        return;
    }

    try
    {
        if (service.Complete(nr))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Taak gemarkeerd als voltooid.");
            Console.ResetColor();
        }
        else
        {
            ShowError("Nummer niet gevonden.");
        }
    }
    catch (Exception ex)
    {
        ShowError(ex.Message);
    }

    Pause();
}

void DeleteTask()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("── Taak verwijderen ──");
    Console.ResetColor();

    ShowInlineList();

    Console.Write("  Nummer: ");
    if (!int.TryParse(Console.ReadLine(), out int nr))
    {
        ShowError("Ongeldig nummer.");
        Pause();
        return;
    }

    try
    {
        if (service.Delete(nr))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Taak verwijderd.");
            Console.ResetColor();
        }
        else
        {
            ShowError("Nummer niet gevonden.");
        }
    }
    catch (Exception ex)
    {
        ShowError(ex.Message);
    }

    Pause();
}

void SearchTasks()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("── Zoeken ──");
    Console.ResetColor();

    Console.Write("  Zoekterm: ");
    var keyword = Console.ReadLine()?.Trim() ?? string.Empty;

    var results = service.Search(keyword);
    Console.WriteLine();

    if (results.Count == 0)
    {
        Console.WriteLine("  Geen resultaten gevonden.");
    }
    else
    {
        foreach (var task in results)
        {
            Console.ForegroundColor = task.IsCompleted ? ConsoleColor.Green : ConsoleColor.White;
            string status = task.IsCompleted ? "[✓]" : "[ ]";
            Console.WriteLine($"  {status} {task.Title}");
            Console.ResetColor();
        }
    }

    Pause();
}

void ShowInlineList()
{
    var tasks = service.GetAll();
    Console.WriteLine();
    for (int i = 0; i < tasks.Count; i++)
    {
        var task = tasks[i];
        Console.ForegroundColor = task.IsCompleted ? ConsoleColor.Green : ConsoleColor.White;
        string status = task.IsCompleted ? "[✓]" : "[ ]";
        Console.WriteLine($"  {i + 1,2}. {status} {task.Title}");
        Console.ResetColor();
    }
    Console.WriteLine();
}

void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"  Fout: {message}");
    Console.ResetColor();
}

void Pause()
{
    Console.WriteLine();
    Console.Write("  Druk op Enter om terug te gaan...");
    Console.ReadLine();
}
