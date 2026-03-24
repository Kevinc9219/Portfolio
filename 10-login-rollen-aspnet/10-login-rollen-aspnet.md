# 🔐 Gebruikersbeheer met Login & Rollen – ASP.NET Identity

Een webapplicatie met volledig uitgewerkt gebruikersbeheer via ASP.NET Core Identity. Demonstreert authenticatie, autorisatie en rolgebaseerde toegang — essentiële skills voor elke .NET developer.

---

## 🚀 Features

- 📝 Registreren & inloggen
- 🔐 Wachtwoord vergeten / reset via e-mail
- 👥 Rollen: Admin, Manager, Gebruiker
- 🛡️ Pagina's beveiligd per rol
- 👤 Profielpagina met avatar
- 📋 Admin panel: gebruikers beheren en rollen toewijzen
- 🔒 Two-Factor Authentication (2FA) optioneel

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| ASP.NET Core | 7.0+ |
| C# | 10+ |
| ASP.NET Core Identity | 7.x |
| Entity Framework Core | 7.x |
| SQL Server / SQLite | - |
| Bootstrap | 5.x |
| SendGrid (e-mail) | optioneel |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/10-login-rollen-aspnet.git

# 2. Packages installeren
dotnet restore

# 3. Database aanmaken (inclusief Identity tabellen)
dotnet ef database update

# 4. Seed-data laden (admin aanmaken)
# Zie Program.cs → SeedData()

# 5. Applicatie starten
dotnet run
```

**Standaard accounts na seeding:**
```
Admin   : admin@demo.com    / Admin123!
Manager : manager@demo.com  / Manager123!
User    : user@demo.com     / User123!
```

---

## 📁 Projectstructuur

```
10-login-rollen-aspnet/
├── Controllers/
│   ├── AccountController.cs
│   ├── AdminController.cs
│   └── HomeController.cs
├── Models/
│   └── ApplicationUser.cs
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   ├── Register.cshtml
│   │   └── ForgotPassword.cshtml
│   ├── Admin/
│   │   └── UserManagement.cshtml
│   └── Shared/
├── Data/
│   ├── AppDbContext.cs
│   └── SeedData.cs
├── Program.cs
└── README.md
```

---

## 🗃️ Rollen & Toegang

| Pagina | Gebruiker | Manager | Admin |
|--------|-----------|---------|-------|
| Home | ✅ | ✅ | ✅ |
| Dashboard | ✅ | ✅ | ✅ |
| Rapporten | ❌ | ✅ | ✅ |
| Gebruikersbeheer | ❌ | ❌ | ✅ |
| Systeeminstellingen | ❌ | ❌ | ✅ |

---

## 💡 Wat ik geleerd heb

- ASP.NET Core Identity volledig opzetten
- Claims-gebaseerde autorisatie met `[Authorize(Roles="Admin")]`
- Rollen aanmaken en toewijzen via code
- Wachtwoord-reset flow implementeren
- Seed data gebruiken voor testaccounts

---

## 🔮 Toekomstige uitbreidingen

- [ ] OAuth login (Google, Microsoft)
- [ ] JWT tokens voor API authenticatie
- [ ] Audit log van alle acties

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
