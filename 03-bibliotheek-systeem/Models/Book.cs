namespace BibliotheekSysteem.Models;

public class Book
{
    private string _title;
    private string _author;
    private string _isbn;
    private int _year;
    private string _genre;
    private bool _isAvailable;

    public string Title { get => _title; set => _title = value; }
    public string Author { get => _author; set => _author = value; }
    public string ISBN { get => _isbn; set => _isbn = value; }
    public int Year { get => _year; set => _year = value; }
    public string Genre { get => _genre; set => _genre = value; }
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

    public Book() { _isAvailable = true; }

    public Book(string title, string author, string isbn, int year, string genre)
    {
        _title = title;
        _author = author;
        _isbn = isbn;
        _year = year;
        _genre = genre;
        _isAvailable = true;
    }

    public virtual string GetInfo()
    {
        string status = _isAvailable ? "Beschikbaar" : "Uitgeleend";
        return $"[{_isbn}] \"{_title}\" door {_author} ({_year}) — {_genre} | {status}";
    }
}
