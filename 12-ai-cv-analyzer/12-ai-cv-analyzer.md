# 📄 AI CV Analyzer – Automatische cv-feedback via OpenAI API

Een webapplicatie gebouwd met ASP.NET Core MVC waarbij gebruikers hun cv kunnen uploaden of plakken, waarna een AI automatisch gestructureerde feedback genereert: wat is sterk, wat ontbreekt, en welke jobs passen bij dit profiel.

> 💡 *Geïnspireerd op een echte use case: ik gebruikte AI-tools tijdens mijn eigen carrièreswitch naar IT. Dit project bouwt die logica na in code.*

---

## 🚀 Features

- 📋 CV uploaden als tekst of PDF
- 🤖 AI-analyse via OpenAI GPT API
- ✅ Gestructureerde feedback: sterktes, zwaktes, ontbrekende elementen
- 🎯 Jobsuggesties op basis van het cv-profiel
- 💡 Concrete verbetervoorstellen per sectie
- 📊 Score per onderdeel (profiel, ervaring, opleiding, skills)
- 🔐 Veilig sleutelbeheer via appsettings

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| ASP.NET Core MVC | 7.0+ |
| C# | 10+ |
| OpenAI API | gpt-4o-mini |
| iText7 (PDF parsing) | 7.x |
| Bootstrap | 5.x |
| Entity Framework Core | 7.x |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/12-ai-cv-analyzer.git

# 2. Packages installeren
dotnet restore

# 3. API-sleutel instellen
# Maak appsettings.Development.json aan:
{
  "OpenAI": {
    "ApiKey": "sk-jouw-sleutel-hier",
    "Model": "gpt-4o-mini"
  }
}

# 4. Database aanmaken
dotnet ef database update

# 5. App starten
dotnet run

# 6. Open in browser
# https://localhost:5001
```

---

## 📸 Hoe het werkt

```
┌─────────────────────────────────────┐
│         CV Analyzer                 │
│  ┌─────────────────────────────┐    │
│  │ Plak je cv hier...          │    │
│  │                             │    │
│  └─────────────────────────────┘    │
│         [Analyseren →]              │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│  📊 Analyse Resultaat               │
│                                     │
│  ✅ Sterktes                        │
│     • Sterke technische skills      │
│     • Goede werkervaring            │
│                                     │
│  ⚠️  Verbeterpunten                 │
│     • Voeg LinkedIn URL toe         │
│     • Profiel is te kort            │
│                                     │
│  🎯 Passende functies               │
│     • Junior .NET Developer         │
│     • IT Support Engineer           │
└─────────────────────────────────────┘
```

---

## 📁 Projectstructuur

```
12-ai-cv-analyzer/
├── Controllers/
│   └── CvController.cs
├── Models/
│   ├── CvAnalysis.cs
│   └── AnalysisResult.cs
├── Services/
│   ├── OpenAIService.cs
│   └── PdfParserService.cs
├── Views/
│   ├── Cv/
│   │   ├── Index.cshtml
│   │   └── Result.cshtml
│   └── Shared/
├── Data/
│   └── AppDbContext.cs
├── Prompts/
│   └── cv-analysis-prompt.txt
├── appsettings.json
├── .gitignore
└── README.md
```

---

## 🧠 AI Prompt Engineering

De kern van dit project is de prompt die naar OpenAI gestuurd wordt:

```
Analyseer het volgende cv als ervaren HR-professional en geef:
1. Top 3 sterktes van dit profiel
2. Top 3 verbeterpunten met concrete suggesties
3. Ontbrekende elementen (LinkedIn, foto, etc.)
4. Score per sectie: profiel, ervaring, opleiding, skills (op 10)
5. 3 passende functies op basis van dit profiel

Antwoord in JSON-formaat.
CV: [cv-tekst]
```

---

## 💡 Wat ik geleerd heb

- Prompt engineering voor gestructureerde JSON-output
- PDF-tekst extraheren via iText7
- AI-responses parsen en weergeven in een webinterface
- Foutafhandeling bij API-timeouts en ongeldige responses
- Gebruiksvriendelijke UI bouwen voor AI-resultaten

---

## 🔮 Toekomstige uitbreidingen

- [ ] Vergelijking van cv met een specifieke vacature
- [ ] Automatisch verbeterd cv genereren
- [ ] Meerdere cv's opslaan en vergelijken
- [ ] Multi-language support

---

## 🔗 Nuttige links

- [OpenAI API Documentatie](https://platform.openai.com/docs)
- [iText7 voor PDF parsing](https://itextpdf.com/products/itext-7)

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
