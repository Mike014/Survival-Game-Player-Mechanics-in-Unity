# Survival Game – Player Mechanics (Unity)

This repository contains a **learning project built in Unity** focused on the core player mechanics commonly found in **survival games**.  
It serves as a practical foundation for understanding how player systems, environment interaction, and basic survival logic are implemented in a 3D game context.

The project follows a structured approach, starting from player movement and camera control, and gradually introducing survival-oriented systems such as needs, health management, UI feedback, and environmental setup.

---

## Project Overview

Survival games often share a common set of mechanics regardless of their setting (underwater, medieval, sci-fi, etc.).  
This project explores those **foundational systems**, providing reusable patterns that can be applied to many different game genres.

The repository is intended for:
- Learning and practice
- Prototyping survival gameplay systems
- Understanding Unity workflows for player-centric mechanics

---

## Core Features

### Player & Camera
- First-person player controller
- Mouse-driven camera look (horizontal and vertical rotation)
- Physics-based movement using Rigidbody
- Jumping with ground detection via Raycasts

### Survival Systems
- Player needs system:
  - Health
  - Hunger
  - Thirst
  - Sleep
- Continuous decay and regeneration logic
- Health penalties when basic needs are not met
- Damage handling and death conditions

### User Interface (HUD)
- On-screen HUD displaying player needs
- Dynamic UI bars driven by gameplay values
- Crosshair for first-person interaction
- Damage feedback overlay (screen flash on hit)

### Environment Setup
- 3D terrain and island environment
- Water and terrain materials
- Lighting and skybox configuration
- Basic hazards (e.g. damaging objects)

---

## Technical Focus

This project emphasizes:
- Clean separation between gameplay logic and UI
- Physics-based interaction using Unity’s Rigidbody system
- Event-driven design (Unity Events for damage feedback)
- Practical use of Raycasts for grounding and interaction checks
- Input handling using Unity’s Input System

---

## Requirements

- **Unity 6.3 LTS** (recommended)
- Basic to solid knowledge of:
  - Unity Editor
  - C#
  - GameObjects, Components, and Prefabs

This project assumes familiarity with Unity fundamentals and is not intended as a first-ever Unity tutorial.

---

## Educational Purpose

This repository is **not a finished game**, but a **foundation**:
- A starting point for larger survival projects
- A reference for common player-mechanic patterns
- A sandbox for experimenting with extensions such as inventory systems, crafting, AI enemies, or saving/loading

---

## Credits & Source

Based on structured learning material focused on **Survival Game Player Mechanics in Unity**, adapted into a standalone practice repository for educational purposes.

---

## License

This project is intended for learning and educational use.
Refer to the original course and Unity licensing terms for asset usage.

