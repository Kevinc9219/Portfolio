# 🏠 Domotica Dashboard

Een gesimuleerd smart home dashboard gebouwd met HTML, CSS en JavaScript. Bedien virtuele apparaten, bekijk temperatuur en energieverbruik — allemaal in de browser. Geïnspireerd op mijn persoonlijke interesse in domotica en Home Automation.

🔗 **Live demo:** [kevincallaert.github.io/06-domotica-dashboard](https://kevincallaert.github.io/06-domotica-dashboard)

---

## 🚀 Features

- 💡 Lampen aan/uit schakelen per kamer
- 🌡️ Temperatuurweergave per ruimte (gesimuleerd)
- ⚡ Energieverbruik tracker
- 🔒 Simulatie van deur- en raamsloten
- 🕐 Tijdschema's instellen voor apparaten
- 🌙 Dark mode toggle

---

## 🛠️ Technologieën

| Tool | Gebruik |
|------|---------|
| HTML5 | Structuur |
| CSS3 | Styling, dark mode, animaties |
| JavaScript (Vanilla) | Logica, DOM manipulatie, LocalStorage |
| Chart.js | Energieverbruik grafiek |

---

## ▶️ Gebruik

```bash
# 1. Repository klonen
git clone https://github.com/kevincallaert/06-domotica-dashboard.git

# 2. Open index.html in je browser
# Geen installatie nodig!
```

Of bekijk de live demo via GitHub Pages.

---

## 📁 Projectstructuur

```
06-domotica-dashboard/
├── css/
│   ├── style.css
│   └── darkmode.css
├── js/
│   ├── devices.js
│   ├── temperature.js
│   └── schedule.js
├── assets/
│   └── icons/
├── index.html
└── README.md
```

---

## 🏗️ Architectuur

```
index.html
  └── Kamers (Living, Slaapkamer, Keuken, Badkamer)
        └── Apparaten (Lamp, Thermostaat, Slot)
              └── State opgeslagen in LocalStorage
```

---

## 💡 Wat ik geleerd heb

- Complexe UI bouwen met vanilla JavaScript
- State management zonder framework
- LocalStorage gebruiken voor persistentie
- Werken met Chart.js voor datavisualisatie
- CSS custom properties voor dark mode

---

## 🔮 Toekomstige uitbreidingen

- [ ] Koppeling met echte MQTT-broker
- [ ] REST API backend in ASP.NET Core
- [ ] Mobiele app versie

---

## 👤 Auteur

**Kevin Callaert**
🔗 [LinkedIn](https://linkedin.com/in/kevin-callaert-b75125215) · 📧 kevincallaert92@gmail.com
