using BibliotheekSysteem.Models;
using Newtonsoft.Json;

namespace BibliotheekSysteem.Services;

public class LoanService
{
    private List<Loan> _loans = new();
    private const string FilePath = "Data/loans.json";

    public void Load()
    {
        if (!File.Exists(FilePath)) return;
        _loans = JsonConvert.DeserializeObject<List<Loan>>(File.ReadAllText(FilePath)) ?? new();
    }

    public void Save()
    {
        Directory.CreateDirectory("Data");
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(_loans, Formatting.Indented));
    }

    public Loan CreateLoan(string bookIsbn, string memberNumber)
    {
        var loan = new Loan(bookIsbn, memberNumber);
        _loans.Add(loan);
        Save();
        return loan;
    }

    public bool ReturnLoan(string bookIsbn)
    {
        var loan = _loans.FirstOrDefault(l => l.BookIsbn == bookIsbn && !l.IsReturned);
        if (loan == null) return false;
        loan.ReturnDate = DateTime.Today;
        Save();
        return true;
    }

    public List<Loan> GetActiveLoans() => _loans.Where(l => !l.IsReturned).ToList();

    public List<Loan> GetOverdueLoans() => _loans.Where(l => l.IsOverdue).ToList();

    public Loan? GetActiveLoanByIsbn(string isbn) =>
        _loans.FirstOrDefault(l => l.BookIsbn == isbn && !l.IsReturned);
}
