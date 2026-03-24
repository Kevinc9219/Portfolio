# 📦 Voorraadbeheersysteem – ASP.NET MVC

Een volledige webapplicatie voor voorraadbeheer gebouwd met ASP.NET Core MVC. Beheers producten, leveranciers en bestellingen via een overzichtelijke webinterface.

---

## 🚀 Features

- 📦 Producten beheren (CRUD)
- 🏭 Leveranciers beheren
- 🛒 Bestellingen aanmaken en opvolgen
- 📊 Dashboard met voorraadoverzicht en statistieken
- 🔐 Loginpagina (admin-rol)
- 🔍 Zoeken en filteren
- 📱 Responsive Bootstrap interface

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| ASP.NET Core MVC | 7.0+ |
| C# | 10+ |
| Entity Framework Core | 7.x |
| SQL Server Express / SQLite | - |
| Bootstrap | 5.x |
| Razor Views | - |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/08-voorraad-mvc-app.git

# 2. Packages installeren
dotnet restore

# 3. Database aanmaken
dotnet ef database update

# 4. Applicatie starten
dotnet run

# 5. Open in browser
# https://localhost:5001
```

**Standaard inloggegevens:**
```
Gebruiker: admin@demo.com
Wachtwoord: Admin123!
```

---

## 📁 Projectstructuur

```
08-voorraad-mvc-app/
├── Controllers/
│   ├── ProductsController.cs
│   ├── SuppliersController.cs
│   └── OrdersController.cs
├── Models/
│   ├── Product.cs
│   ├── Supplier.cs
│   └── Order.cs
├── Views/
│   ├── Products/
│   ├── Suppliers/
│   ├── Orders/
│   └── Shared/
├── Data/
│   └── AppDbContext.cs
├── Migrations/
├── wwwroot/
├── Program.cs
└── README.md
```

---

## 🗃️ Database Schema

```
Products   (Id, Name, SKU, Price, Stock, SupplierId)
Suppliers  (Id, Name, Email, Phone, Address)
Orders     (Id, ProductId, Quantity, OrderDate, Status)
```

---

## 💡 Wat ik geleerd heb

- MVC-patroon (Model-View-Controller) in ASP.NET Core
- Razor Views bouwen met Bootstrap
- Relaties tussen entiteiten via Entity Framework
- Authenticatie toevoegen met ASP.NET Identity
- Validatie van formulieren (server- en clientzijde)

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
