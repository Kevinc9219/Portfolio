# 📖 Bibliotheek Beheersysteem

Een consoleprogramma in C# voor het beheren van een boekenbibliotheek. Demonstreert object-georiënteerd programmeren met klassen, overerving en interfaces.

---

## 🚀 Features

- 📚 Boeken toevoegen, zoeken en verwijderen
- 👤 Leden registreren
- 🔄 Boeken uitlenen en terugbrengen
- 📊 Overzicht van uitgeleende boeken
- 🔍 Zoeken op titel, auteur of ISBN
- ⚠️ Melding bij te laat inleveren

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| C# | 10+ |
| .NET | 6.0+ |
| OOP principes | Klassen, Overerving, Interfaces |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/03-bibliotheek-systeem.git

# 2. Navigeer naar de map
cd 03-bibliotheek-systeem

# 3. Project starten
dotnet run
```

---

## 📁 Projectstructuur

```
03-bibliotheek-systeem/
├── Models/
│   ├── Book.cs
│   ├── Member.cs
│   └── Loan.cs
├── Interfaces/
│   └── ILoanable.cs
├── Services/
│   ├── BookService.cs
│   └── MemberService.cs
├── Program.cs
└── README.md
```

---

## 🏗️ OOP Concepten gebruikt

| Concept | Toepassing |
|---------|-----------|
| Klassen | Book, Member, Loan |
| Overerving | DigitalBook erft van Book |
| Interface | ILoanable op Book en Magazine |
| Encapsulatie | Private velden met properties |
| Polymorfisme | Verschillende types uitleenbaar |

---

## 💡 Wat ik geleerd heb

- OOP-principes toepassen in een realistisch scenario
- Werken met interfaces en abstracte klassen
- Collecties beheren met List<T> en LINQ
- Datum- en tijdberekeningen in C#

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
