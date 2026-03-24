namespace CvAnalyzer.Services;

public class PdfParserService
{
    public string ExtractText(Stream pdfStream)
    {
        // Vereenvoudigde PDF tekst extractie (zonder iText7 dependency)
        // In productie: gebruik iText7 voor echte PDF parsing
        using var reader = new StreamReader(pdfStream);
        var content = reader.ReadToEnd();
        // Strip binary PDF content, houd leesbare tekst
        var text = System.Text.RegularExpressions.Regex.Replace(content, @"[^\x20-\x7E\r\n]", " ");
        return text.Length > 100 ? text : "PDF kon niet worden gelezen. Plak de CV-tekst handmatig.";
    }
}
