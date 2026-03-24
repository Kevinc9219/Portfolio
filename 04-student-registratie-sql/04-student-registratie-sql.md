# 🗄️ Student Registratiesysteem – C# + SQL

Een Windows Forms applicatie voor het beheren van studentregistraties, gekoppeld aan een SQL-database via Entity Framework Core. Volledig CRUD-systeem met zoek- en filterfuncties.

---

## 🚀 Features

- 👤 Studenten toevoegen, bewerken en verwijderen
- 📚 Cursussen en inschrijvingen beheren
- 🔍 Zoeken en filteren op naam, cursus of jaar
- 📊 Overzichtsscherm met statistieken
- 💾 Persistente opslag via SQL Server / SQLite

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| C# | 10+ |
| .NET | 6.0+ |
| Entity Framework Core | 7.x |
| SQLite | 3.x |
| Windows Forms | - |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/04-student-registratie-sql.git

# 2. NuGet packages installeren
dotnet restore

# 3. Database migratie uitvoeren
dotnet ef database update

# 4. Project starten (Visual Studio of)
dotnet run
```

---

## 📁 Projectstructuur

```
04-student-registratie-sql/
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── Student.cs
│   └── Course.cs
├── Migrations/
├── Forms/
│   ├── MainForm.cs
│   └── StudentForm.cs
├── Program.cs
└── README.md
```

---

## 🗃️ Database Schema

```sql
Students (Id, FirstName, LastName, Email, EnrollmentDate)
Courses  (Id, Name, Description, Credits)
Enrollments (StudentId, CourseId, Grade)
```

---

## 💡 Wat ik geleerd heb

- Entity Framework Core: Code First aanpak
- Database migraties uitvoeren
- LINQ queries schrijven voor databaseoperaties
- Relaties tussen tabellen (1-op-veel, veel-op-veel)

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
