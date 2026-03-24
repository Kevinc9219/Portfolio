namespace BibliotheekSysteem.Models;

public class Loan
{
    private string _loanId;
    private string _bookIsbn;
    private string _memberNumber;
    private DateTime _borrowDate;
    private DateTime _dueDate;
    private DateTime? _returnDate;

    public string LoanId { get => _loanId; set => _loanId = value; }
    public string BookIsbn { get => _bookIsbn; set => _bookIsbn = value; }
    public string MemberNumber { get => _memberNumber; set => _memberNumber = value; }
    public DateTime BorrowDate { get => _borrowDate; set => _borrowDate = value; }
    public DateTime DueDate { get => _dueDate; set => _dueDate = value; }
    public DateTime? ReturnDate { get => _returnDate; set => _returnDate = value; }

    public bool IsReturned => _returnDate.HasValue;
    public bool IsOverdue => !IsReturned && DateTime.Today > _dueDate;

    public Loan() { }

    public Loan(string bookIsbn, string memberNumber)
    {
        _loanId = Guid.NewGuid().ToString("N")[..8].ToUpper();
        _bookIsbn = bookIsbn;
        _memberNumber = memberNumber;
        _borrowDate = DateTime.Today;
        _dueDate = DateTime.Today.AddDays(14);
        _returnDate = null;
    }
}
