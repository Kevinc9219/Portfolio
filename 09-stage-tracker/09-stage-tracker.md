# 🤝 Stage Tracker – Mijn Eigen Stageopvolging

Een persoonlijke applicatie om mijn verplichte stagedagen (60 dagen) bij te houden als onderdeel van het Graduaat Programmeren aan Odisee Hogeschool. Gebouwd voor mezelf, maar bruikbaar voor elke student met een stageplicht.

> 💡 *Dit project is geboren uit een echte nood — ik heb 60 stagedagen nodig en wil die professioneel opvolgen.*

---

## 🚀 Features

- 📅 Stagedagen registreren (datum, uren, activiteiten)
- 📊 Voortgangsbalk: X van 60 dagen voltooid
- 🏢 Stagebedrijf en contactpersoon bijhouden
- 📝 Dagboek per stagedag: wat heb ik geleerd?
- 🎯 Competenties opvolgen
- 📤 Exporteren naar PDF (stageverslag)

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| C# | 10+ |
| ASP.NET Core MVC | 7.0+ |
| Entity Framework Core | 7.x |
| SQLite | 3.x |
| Bootstrap | 5.x |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/09-stage-tracker.git

# 2. Packages installeren
dotnet restore

# 3. Database aanmaken
dotnet ef database update

# 4. App starten
dotnet run

# 5. Open in browser
# https://localhost:5001
```

---

## 📁 Projectstructuur

```
09-stage-tracker/
├── Controllers/
│   ├── DaysController.cs
│   └── CompanyController.cs
├── Models/
│   ├── StageDay.cs
│   ├── Company.cs
│   └── Competence.cs
├── Views/
│   ├── Dashboard/
│   ├── Days/
│   └── Shared/
├── Data/
│   └── AppDbContext.cs
├── Program.cs
└── README.md
```

---

## 📊 Dashboard voorbeeld

```
Voortgang: ████████████░░░░░░░░ 28/60 dagen (47%)

Huidig bedrijf : [Bedrijfsnaam]
Contactpersoon : [Naam]
Startdatum     : 01/09/2025
Verwacht einde : 28/11/2025
```

---

## 💡 Wat ik geleerd heb

- Een echte use case omzetten naar een werkende applicatie
- Datum- en tijdberekeningen in C#
- Voortgangslogica implementeren
- PDF-export via een library
- Van idee naar product: volledig eigenhandig gebouwd

---

## 🎯 Waarom dit project

Als student met een verplichte stage van 60 dagen wilde ik een tool die mij helpt dit professioneel op te volgen. In plaats van een Excel-sheet te gebruiken, bouwde ik een eigen webapplicatie. Dit project toont aan dat ik in staat ben om een reëel probleem op te lossen met code.

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
