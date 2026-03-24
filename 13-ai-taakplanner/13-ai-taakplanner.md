# 🧠 AI Taakplanner – Slimme prioritering via OpenAI API

Een intelligente to-do applicatie waarbij je taken beschrijft in gewone taal en de AI automatisch prioriteit toekent, tijdsduur inschat en taken groepeert per categorie. Gebouwd met ASP.NET Core MVC en OpenAI API.

> 💡 *Dit project bouwt verder op mijn eenvoudige to-do console app (project 1) en voegt AI-intelligentie toe — een perfecte demonstratie van hoe AI bestaande software slimmer maakt.*

---

## 🚀 Features

- ✍️ Taken invoeren in gewone taal ("Vergadering voorbereiden voor vrijdag")
- 🤖 AI analyseert automatisch:
  - 🔴 Prioriteit (Hoog / Midden / Laag)
  - ⏱️ Geschatte tijdsduur
  - 🏷️ Categorie (Werk, Persoonlijk, Studie, Administratie...)
  - 📅 Deadline detectie uit de tekst
- 📊 Dashboard gesorteerd op AI-prioriteit
- ✅ AI-suggestie accepteren of handmatig overschrijven
- 📈 Productiviteitsstatistieken per week

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| ASP.NET Core MVC | 7.0+ |
| C# | 10+ |
| OpenAI API | gpt-4o-mini |
| Entity Framework Core | 7.x |
| SQLite | 3.x |
| Bootstrap | 5.x |
| JavaScript | Vanilla (AJAX calls) |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/13-ai-taakplanner.git

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
Jij typt: "Belastingaangifte indienen voor eind maart"
                    ↓
         AI analyseert de taak
                    ↓
┌──────────────────────────────────────┐
│  🤖 AI Analyse                       │
│                                      │
│  Taak     : Belastingaangifte        │
│  Prioriteit: 🔴 HOOG                 │
│  Categorie : Administratie           │
│  Duur      : ~2 uur                  │
│  Deadline  : 31 maart (gedetecteerd) │
│                                      │
│  [✅ Accepteren]  [✏️ Aanpassen]      │
└──────────────────────────────────────┘
```

---

## 📁 Projectstructuur

```
13-ai-taakplanner/
├── Controllers/
│   ├── TasksController.cs
│   └── DashboardController.cs
├── Models/
│   ├── TaskItem.cs
│   └── AiAnalysis.cs
├── Services/
│   ├── OpenAIService.cs
│   └── TaskAnalyzerService.cs
├── Views/
│   ├── Tasks/
│   │   ├── Index.cshtml
│   │   └── Create.cshtml
│   ├── Dashboard/
│   │   └── Index.cshtml
│   └── Shared/
├── Data/
│   └── AppDbContext.cs
├── appsettings.json
├── .gitignore
└── README.md
```

---

## 🗃️ Database Schema

```sql
Tasks (
  Id, Title, Description,
  AiPriority, UserPriority,
  AiCategory, UserCategory,
  AiDuration, Deadline,
  IsCompleted, CreatedAt
)
```

---

## 🧠 AI Prompt Engineering

```
Analyseer de volgende taak en geef JSON terug met:
- priority: "hoog" | "midden" | "laag"
- category: "werk" | "persoonlijk" | "studie" | "administratie" | "gezondheid"
- estimatedMinutes: getal (geschatte tijdsduur in minuten)
- deadline: datum als ISO string of null (als vermeld in de tekst)
- reasoning: korte uitleg van de prioriteitskeuze

Taak: "[taak-beschrijving]"
Datum van vandaag: [datum]
```

---

## 💡 Wat ik geleerd heb

- AI gebruiken als beslissingsondersteuning (niet als vervanging)
- JSON-responses van OpenAI parsen naar C# objecten
- AJAX calls vanuit JavaScript naar ASP.NET Core API
- UX-design: AI-suggesties tonen zonder de gebruiker te overweldigen
- Omgaan met AI-fouten en fallback-logica inbouwen

---

## 🤔 Design beslissingen

**Waarom kan de gebruiker de AI overschrijven?**
AI maakt fouten. Door de gebruiker controle te geven bouw ik vertrouwen in de applicatie — en dat is een goede software-filosofie.

**Waarom gpt-4o-mini en niet gpt-4?**
Voor eenvoudige classificatietaken is gpt-4o-mini sneller, goedkoper en meer dan nauwkeurig genoeg. Kosten-bewust programmeren is ook een skill.

---

## 🔮 Toekomstige uitbreidingen

- [ ] Dagelijkse AI-planning: "Wat moet ik vandaag doen?"
- [ ] Integratie met Google Calendar
- [ ] Slimme herinneringen op basis van deadline en prioriteit
- [ ] Teamtaken met AI-toewijzing per persoon

---

## 🔗 Nuttige links

- [OpenAI API Documentatie](https://platform.openai.com/docs)
- [ASP.NET Core MVC](https://docs.microsoft.com/aspnet/core/mvc)

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
