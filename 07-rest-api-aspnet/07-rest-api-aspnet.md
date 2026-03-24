# 🔗 REST API – ASP.NET Core Web API

Een RESTful Web API gebouwd met ASP.NET Core voor het beheren van een filmcatalogus. Volledig gedocumenteerd via Swagger UI, met CRUD-endpoints en database integratie.

---

## 🚀 Endpoints

| Method | Endpoint | Beschrijving |
|--------|----------|--------------|
| GET | `/api/movies` | Alle films ophalen |
| GET | `/api/movies/{id}` | Film op ID ophalen |
| GET | `/api/movies?genre=Action` | Films filteren op genre |
| POST | `/api/movies` | Nieuwe film toevoegen |
| PUT | `/api/movies/{id}` | Film bijwerken |
| DELETE | `/api/movies/{id}` | Film verwijderen |

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| ASP.NET Core | 7.0+ |
| C# | 10+ |
| Entity Framework Core | 7.x |
| SQLite | 3.x |
| Swagger / Swashbuckle | 6.x |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/07-rest-api-aspnet.git

# 2. Packages installeren
dotnet restore

# 3. Database aanmaken
dotnet ef database update

# 4. API starten
dotnet run

# 5. Swagger UI openen
# Ga naar: https://localhost:5001/swagger
```

---

## 📸 Swagger UI

Na het starten is de volledige API documentatie beschikbaar op `/swagger`:

```
GET    /api/movies         → Lijst van alle films
POST   /api/movies         → Film toevoegen (JSON body)
GET    /api/movies/{id}    → Specifieke film
PUT    /api/movies/{id}    → Film aanpassen
DELETE /api/movies/{id}    → Film verwijderen
```

---

## 📁 Projectstructuur

```
07-rest-api-aspnet/
├── Controllers/
│   └── MoviesController.cs
├── Models/
│   └── Movie.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── MovieCreateDto.cs
│   └── MovieResponseDto.cs
├── Migrations/
├── Program.cs
├── appsettings.json
└── README.md
```

---

## 📦 Voorbeeld JSON Request

```json
POST /api/movies
{
  "title": "Inception",
  "genre": "Sci-Fi",
  "year": 2010,
  "director": "Christopher Nolan",
  "rating": 8.8
}
```

---

## 💡 Wat ik geleerd heb

- RESTful API design principes
- ASP.NET Core Web API opzetten
- DTOs gebruiken voor data-overdracht
- Swagger documentatie genereren
- HTTP statuscodes correct toepassen

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
