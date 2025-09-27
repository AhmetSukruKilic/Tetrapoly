# Tetrapoly

Tetrapoly is a Unity-based 3D board game inspired by Monopoly.  
The goal is simple: roll dice, move your car token, buy and manage districts, and compete to become the wealthiest player on the board. The project combines board game mechanics with Unity’s physics and UI systems to create an interactive digital version of a city-building economy game.

---

## 🎮 Features
- **3D Grid Board** – a Monopoly-style board built in Unity  
- **Dice Rolling System** – realistic dice mechanics using Unity physics  
- **Player Movement** – car tokens move around the grid according to dice rolls  
- **Districts & Properties** – buy, own, and compete for districts with set prices  
- **Game UI** – simple interface for dice rolls, ownership status, and player turns  
- **Expandable Logic** – easy to extend for new rules, cards, or multiplayer support  

---

## 🛠️ Getting Started

### Prerequisites
- Unity 2022.3 (or newer recommended)  
- .NET / C# support (comes with Unity)  

### Installation
1. Clone the repository:  
   ```bash
   git clone https://github.com/AhmetSukruKilic/Tetrapoly.git

Open the project in Unity Hub.
Load the main scene located in:
  Assets/Scenes/MainScene.unity
Press Play to start the game!

Assets/
  ├── Scenes/       # Main Unity scenes
  ├── Scripts/      # C# scripts (game logic, dice, board)
  ├── Prefabs/      # Reusable game objects (cars, districts, dice)
  ├── Materials/    # Visual styles and colors
  └── UI/           # User interface elements
ProjectSettings/    # Unity configuration
