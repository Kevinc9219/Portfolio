using BibliotheekSysteem.Models;
using BibliotheekSysteem.Services;

var bookService = new BookService();
var memberService = new MemberService();
var loanService = new LoanService();

bookService.Load();
memberService.Load();
loanService.Load();

SeedDemoData();
RunMainMenu();

// ─── Main menu ────────────────────────────────────────────────────────────────

void RunMainMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║     BIBLIOTHEEK BEHEERSYSTEEM        ║");
        Console.WriteLine("╠══════════════════════════════════════╣");
        Console.WriteLine("║  1. Boek toevoegen                   ║");
        Console.WriteLine("║  2. Lid registreren                  ║");
        Console.WriteLine("║  3. Boek uitlenen                    ║");
        Console.WriteLine("║  4. Boek terugbrengen                ║");
        Console.WriteLine("║  5. Zoeken (titel / auteur / ISBN)   ║");
        Console.WriteLine("║  6. Alle uitgeleende boeken          ║");
        Console.WriteLine("║  7. Alle boeken                      ║");
        Console.WriteLine("║  8. Alle leden                       ║");
        Console.WriteLine("║  0. Afsluiten                        ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.Write("\nKeuze: ");

        switch (Console.ReadLine()?.Trim())
        {
            case "1": AddBook(); break;
            case "2": AddMember(); break;
            case "3": LendBook(); break;
            case "4": ReturnBook(); break;
            case "5": SearchBooks(); break;
            case "6": ShowActiveLoans(); break;
            case "7": ShowAllBooks(); break;
            case "8": ShowAllMembers(); break;
            case "0": Console.WriteLine("\nTot ziens!"); return;
            default: Pause("Ongeldige keuze."); break;
        }
    }
}

// ─── Menu actions ─────────────────────────────────────────────────────────────

void AddBook()
{
    Console.Clear();
    Console.WriteLine("=== BOEK TOEVOEGEN ===\n");

    Console.Write("Digitaal boek? (j/n): ");
    bool isDigital = Console.ReadLine()?.Trim().ToLower() == "j";

    string title  = Prompt("Titel: ");
    string author = Prompt("Auteur: ");
    string isbn   = Prompt("ISBN: ");
    int year      = PromptInt("Jaar: ");
    string genre  = Prompt("Genre: ");

    if (isDigital)
    {
        string url = Prompt("Download URL: ");
        bookService.AddBook(new DigitalBook(title, author, isbn, year, genre, url));
    }
    else
    {
        bookService.AddBook(new Book(title, author, isbn, year, genre));
    }

    Console.WriteLine("\nBoek toegevoegd!");
    Pause();
}

void AddMember()
{
    Console.Clear();
    Console.WriteLine("=== LID REGISTREREN ===\n");

    string name  = Prompt("Naam: ");
    string email = Prompt("E-mail: ");
    string number = memberService.GenerateMemberNumber();

    memberService.AddMember(new Member(name, email, number));

    Console.WriteLine($"\nLid geregistreerd met nummer: {number}");
    Pause();
}

void LendBook()
{
    Console.Clear();
    Console.WriteLine("=== BOEK UITLENEN ===\n");

    string isbn   = Prompt("ISBN van het boek: ");
    string memNum = Prompt("Lidnummer: ");

    var book   = bookService.FindByIsbn(isbn);
    var member = memberService.FindByNumber(memNum);

    if (book == null)   { Pause("Boek niet gevonden."); return; }
    if (member == null) { Pause("Lid niet gevonden."); return; }
    if (!book.IsAvailable) { Pause("Dit boek is momenteel niet beschikbaar."); return; }

    bookService.SetCurrentBook(book);
    if (bookService.Borrow(member))
    {
        var loan = loanService.CreateLoan(isbn, memNum);
        Console.WriteLine($"\nBoek uitgeleend aan {member.Name}.");
        Console.WriteLine($"Terugbrengdatum: {loan.DueDate:dd-MM-yyyy}");
    }
    Pause();
}

void ReturnBook()
{
    Console.Clear();
    Console.WriteLine("=== BOEK TERUGBRENGEN ===\n");

    string isbn = Prompt("ISBN van het boek: ");

    var book = bookService.FindByIsbn(isbn);
    if (book == null) { Pause("Boek niet gevonden."); return; }

    var loan = loanService.GetActiveLoanByIsbn(isbn);
    if (loan == null) { Pause("Geen actieve uitleen gevonden voor dit boek."); return; }

    if (loan.IsOverdue)
    {
        int days = (DateTime.Today - loan.DueDate).Days;
        Console.WriteLine($"\n  TE LAAT — {days} dag(en) over de terugbreektermijn!");
    }

    bookService.SetCurrentBook(book);
    bookService.Return();
    loanService.ReturnLoan(isbn);

    Console.WriteLine("\nBoek succesvol teruggebracht.");
    Pause();
}

void SearchBooks()
{
    Console.Clear();
    Console.WriteLine("=== ZOEKEN ===\n");

    string query = Prompt("Zoekterm (titel / auteur / ISBN): ");
    var results = bookService.Search(query);

    Console.WriteLine($"\n{results.Count} resultaat/resultaten:\n");
    foreach (var b in results)
        Console.WriteLine("  " + b.GetInfo());

    Pause();
}

void ShowActiveLoans()
{
    Console.Clear();
    Console.WriteLine("=== UITGELEENDE BOEKEN ===\n");

    var activeLoans = loanService.GetActiveLoans();
    if (!activeLoans.Any()) { Pause("Geen boeken momenteel uitgeleend."); return; }

    foreach (var loan in activeLoans)
    {
        var book   = bookService.FindByIsbn(loan.BookIsbn);
        var member = memberService.FindByNumber(loan.MemberNumber);

        string overdueTag = loan.IsOverdue
            ? $"  *** TE LAAT ({(DateTime.Today - loan.DueDate).Days} dagen) ***"
            : "";

        Console.WriteLine($"  [{loan.LoanId}] {book?.Title ?? loan.BookIsbn}");
        Console.WriteLine($"         Lid: {member?.Name ?? loan.MemberNumber}");
        Console.WriteLine($"         Uitgeleend: {loan.BorrowDate:dd-MM-yyyy}  |  Terug voor: {loan.DueDate:dd-MM-yyyy}{overdueTag}");
        Console.WriteLine();
    }

    Pause();
}

void ShowAllBooks()
{
    Console.Clear();
    Console.WriteLine("=== ALLE BOEKEN ===\n");

    var books = bookService.GetAll();
    if (!books.Any()) { Pause("Geen boeken in het systeem."); return; }

    foreach (var b in books)
        Console.WriteLine("  " + b.GetInfo());

    Pause();
}

void ShowAllMembers()
{
    Console.Clear();
    Console.WriteLine("=== ALLE LEDEN ===\n");

    var members = memberService.GetAll();
    if (!members.Any()) { Pause("Geen leden geregistreerd."); return; }

    foreach (var m in members)
        Console.WriteLine("  " + m);

    Pause();
}

// ─── Helpers ──────────────────────────────────────────────────────────────────

string Prompt(string label)
{
    Console.Write(label);
    return Console.ReadLine()?.Trim() ?? "";
}

int PromptInt(string label)
{
    while (true)
    {
        Console.Write(label);
        if (int.TryParse(Console.ReadLine(), out int val)) return val;
        Console.WriteLine("Voer een geldig getal in.");
    }
}

void Pause(string? message = null)
{
    if (message != null) Console.WriteLine("\n" + message);
    Console.Write("\nDruk op Enter om verder te gaan...");
    Console.ReadLine();
}

// ─── Demo seed ────────────────────────────────────────────────────────────────

void SeedDemoData()
{
    if (bookService.GetAll().Any()) return; // already seeded

    bookService.AddBook(new Book("De Aanslag", "Harry Mulisch", "9789023413950", 1982, "Roman"));
    bookService.AddBook(new Book("Het Achterhuis", "Anne Frank", "9789048839124", 1947, "Dagboek"));
    bookService.AddBook(new DigitalBook("Clean Code", "Robert C. Martin", "9780132350884", 2008, "Technisch", "https://example.com/cleancode.epub"));
    bookService.AddBook(new Book("De Ontdekking van de Hemel", "Harry Mulisch", "9789023456872", 1992, "Roman"));

    memberService.AddMember(new Member("Kevin Callaert", "kevin@example.com", "LID0001"));
    memberService.AddMember(new Member("Jana Pieters", "jana@example.com", "LID0002"));
}
