# 🤖 AI Chatbot – C# integratie met OpenAI GPT API

Een interactieve chatbot gebouwd in C# die communiceert via de OpenAI API. De bot onthoudt de gesprekshistoriek, heeft een instelbare persona en kan worden uitgebreid naar een ASP.NET Core webinterface.

> 💡 *Dit project toont aan hoe je een externe AI-API integreert in een .NET applicatie — een steeds meer gevraagde skill in de moderne IT-sector.*

---

## 🚀 Features

- 💬 Conversatie met GPT via OpenAI API
- 🧠 Gesprekshistoriek bijhouden (context-aware antwoorden)
- 🎭 Instelbare persona via systeem-prompt
- 🌡️ Temperatuur instellen (creatief ↔ nauwkeurig)
- 🔐 Veilig beheer van API-sleutels via `appsettings.json`
- 🖥️ Console interface (uitbreidbaar naar ASP.NET Core web)

---

## 🛠️ Technologieën

| Tool | Versie |
|------|--------|
| C# | 10+ |
| .NET | 6.0+ |
| OpenAI API | gpt-4o-mini |
| Azure.AI.OpenAI / OpenAI NuGet | 1.x |
| ASP.NET Core (optioneel) | 7.0+ |

---

## ▶️ Installatie & Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/11-ai-chatbot-openai.git

# 2. NuGet packages installeren
dotnet restore

# 3. API-sleutel instellen in appsettings.json
# (zie sectie "API-sleutel instellen" hieronder)

# 4. Starten
dotnet run
```

### 🔑 API-sleutel instellen

Maak een bestand `appsettings.Development.json` aan (staat in .gitignore!):

```json
{
  "OpenAI": {
    "ApiKey": "sk-jouw-sleutel-hier",
    "Model": "gpt-4o-mini"
  }
}
```

> ⚠️ Commit je API-sleutel NOOIT naar GitHub. Controleer altijd je `.gitignore`.

---

## 📸 Demo

```
╔══════════════════════════════════════╗
║   AI Chatbot – Powered by OpenAI    ║
╚══════════════════════════════════════╝
Bot: Hallo! Ik ben je AI-assistent. Hoe kan ik je helpen?

Jij: Wat is het verschil tussen C# en Java?
Bot: Zowel C# als Java zijn objectgeoriënteerde talen...
     [gedetailleerd antwoord]

Jij: Geef me een codevoorbeeld in C#
Bot: Hier is een voorbeeld... [antwoord met context van vorige vraag]
```

---

## 📁 Projectstructuur

```
11-ai-chatbot-openai/
├── Models/
│   └── ChatMessage.cs
├── Services/
│   └── OpenAIService.cs
├── appsettings.json
├── appsettings.Development.json  ← in .gitignore!
├── .gitignore
├── Program.cs
└── README.md
```

---

## 🔑 Hoe OpenAI API werkt

```
Gebruiker typt bericht
       ↓
Voeg bericht toe aan gesprekshistoriek
       ↓
Stuur volledige history naar OpenAI API
       ↓
Ontvang antwoord → toon aan gebruiker
       ↓
Voeg antwoord toe aan history
       ↓
Herhaal
```

---

## 🔮 Toekomstige uitbreidingen

- [ ] Webinterface via ASP.NET Core + SignalR (live chat)
- [ ] Streaming responses (tekst verschijnt letter per letter)
- [ ] Meerdere persona's kiezen (assistent, tutor, coach)
- [ ] Gesprekken opslaan in database

---

## 💡 Wat ik geleerd heb

- Werken met externe REST API's via HttpClient in C#
- OpenAI API integreren met NuGet package
- Gesprekscontext beheren (rollen: system, user, assistant)
- API-sleutels veilig beheren met .gitignore en appsettings
- Prompt engineering: systeem-prompts schrijven voor specifiek gedrag

---

## 🔗 Nuttige links

- [OpenAI API Documentatie](https://platform.openai.com/docs)
- [OpenAI .NET SDK](https://github.com/openai/openai-dotnet)
- [Gratis API credits aanvragen](https://platform.openai.com/signup)

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
