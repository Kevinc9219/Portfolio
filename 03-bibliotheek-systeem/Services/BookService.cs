using BibliotheekSysteem.Interfaces;
using BibliotheekSysteem.Models;
using Newtonsoft.Json;

namespace BibliotheekSysteem.Services;

public class BookService : ILoanable
{
    private List<Book> _books = new();
    private const string FilePath = "Data/books.json";

    // ILoanable state for current operation
    private Book? _currentBook;

    public void Load()
    {
        if (!File.Exists(FilePath)) return;
        var json = File.ReadAllText(FilePath);
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
        _books = JsonConvert.DeserializeObject<List<Book>>(json, settings) ?? new();
    }

    public void Save()
    {
        Directory.CreateDirectory("Data");
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(_books, settings));
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
        Save();
    }

    public List<Book> GetAll() => _books;

    public Book? FindByIsbn(string isbn) =>
        _books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));

    public List<Book> Search(string query) =>
        _books.Where(b =>
            b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.ISBN.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();

    // ILoanable — sets context book before calling Borrow/Return
    public void SetCurrentBook(Book book) => _currentBook = book;

    public bool Borrow(Member member)
    {
        if (_currentBook == null || !_currentBook.IsAvailable) return false;
        _currentBook.IsAvailable = false;
        Save();
        return true;
    }

    public bool Return()
    {
        if (_currentBook == null || _currentBook.IsAvailable) return false;
        _currentBook.IsAvailable = true;
        Save();
        return true;
    }
}
