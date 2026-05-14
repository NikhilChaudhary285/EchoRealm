# 🧩 Modular Physics Puzzle Framework — Unity URP

### Unity URP • Modular Puzzle Architecture • Event-Driven Systems

A modular physics-based puzzle prototype built in Unity, focused on clean architecture, reusable gameplay systems, and scalable interaction design.

---

# ▶️ How to Run

1. Open the project in **Unity 6000.3.11f1 (URP)**  
2. Open scene: `Assets/Scenes/PuzzleLevel.unity`  
3. Press **Play**  

---

# 🎮 Controls

| Key | Action |
|------|---------|
| WASD | Move |
| Space | Jump |
| E | Interact (Lever) |
| R | Reset Puzzle |

---

# 🧠 Puzzle Solutions

The level is intentionally designed with multiple solution paths.

## Path A — Lever First

1. Activate the lever  
2. Ride the moving platform  
3. Cross the level and win  

## Path B — Box First

1. Push the heavy box onto the pressure plate  
2. Gate opens automatically  
3. Cross the level and finish the puzzle  

---

# 🏗 System Architecture

The project follows a modular and interface-driven architecture to make adding new puzzle mechanics simple and scalable.

---

# 🔌 Core Interfaces

## `IInteractable`

Used by any object capable of changing state and broadcasting events.

### Responsibilities

- Fires `OnStateChanged`
- Handles interaction state changes
- Acts as an event source

### Examples

- Lever
- Pressure Plate
- Timed Trigger

---

## `IReceiver`

Used by any object that reacts to activations or deactivations.

### Core Methods

- `OnActivated()`
- `OnDeactivated()`

### Examples

- Gate
- Moving Platform
- Elevator
- Trap Systems

---

## `IResettable`

Used by objects that must restore themselves to an initial state when resetting the puzzle.

### Core Method

- `ResetToInitial()`

---

# 🔄 InteractionConnector

The `InteractionConnector` acts like a gameplay “wire” between systems.

It listens to an `IInteractable` and forwards activation/deactivation events to one or more `IReceiver` targets.

## Benefits

- Fully data-driven via Inspector  
- No hardcoded references  
- Easily scalable  
- No additional code required for new connections  

---

# ♻️ Reset System

The `ResetManager` automatically restores the puzzle state.

## How It Works

- Uses `FindObjectsByType<MonoBehaviour>()`
- Detects all objects implementing `IResettable`
- Calls `ResetToInitial()` automatically

## Benefits

- No manual registration  
- Easy scalability  
- Centralized reset logic  

---

# ➕ Adding New Gameplay Systems

## Add a New Interactable

Example: Timed Trigger

### Steps

1. Extend `InteractableBase`
2. Call `SetState(true/false)` when triggered
3. Done — `InteractionConnector` handles communication automatically

---

## Add a New Receiver

Example: Spinning Blade / Elevator

### Steps

1. Extend `ReceiverBase`
2. Override:
   - `OnActivated()`
   - `OnDeactivated()`
3. Assign it inside any `InteractionConnector`

---

# 🚀 Extension Examples

| Feature | Implementation Approach |
|----------|--------------------------|
| Timed Trigger | Extend `InteractableBase` and trigger `SetState()` using a timer |
| Elevator | Extend `ReceiverBase` and animate movement in `OnActivated()` |
| Multi-Gate Puzzle | Add multiple receivers to `InteractionConnector.receiverTargets[]` |
| AND Logic Puzzle | Create `MultiInputConnector` that validates all inputs |

---

# 🛠 Tech Stack

- Unity  
- C#  
- Universal Render Pipeline (URP)  
- Physics-Based Gameplay  
- Event-Driven Architecture  
- Interface-Based Design  

---

# 📚 Key Learnings

This project helped improve understanding of:

- Modular gameplay architecture  
- Event-driven communication systems  
- Interface-based programming  
- Reusable puzzle systems  
- Scalable interaction design  
- Resettable gameplay state management  
- Data-driven Unity workflows  

---

## 🔗 Links

🎮 **Gameplay Video**  
https://drive.google.com/file/d/13KUdexL0Ib0fUkV8topen8W_HAT3_5md/view?usp=sharing

💻 **GitHub Repository**  
https://github.com/NikhilChaudhary285/EchoRealm

---
