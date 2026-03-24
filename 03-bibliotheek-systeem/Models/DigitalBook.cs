namespace BibliotheekSysteem.Models;

public class DigitalBook : Book
{
    private string _downloadUrl;

    public string DownloadUrl { get => _downloadUrl; set => _downloadUrl = value; }

    public DigitalBook() { }

    public DigitalBook(string title, string author, string isbn, int year, string genre, string downloadUrl)
        : base(title, author, isbn, year, genre)
    {
        _downloadUrl = downloadUrl;
    }

    public override string GetInfo()
    {
        string status = IsAvailable ? "Beschikbaar" : "Uitgeleend";
        return $"[{ISBN}] \"{Title}\" door {Author} ({Year}) — {Genre} | {status} | Download: {_downloadUrl}";
    }
}
