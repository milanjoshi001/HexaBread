# 🍞 HexaBread

> **A cozy hex-based puzzle game where you stack, merge, and serve delicious treats!**

HexaBread is a cozy, casual puzzle game built around a satisfying **hexagonal stacking and merging mechanic**. Players strategically place food items onto a hexagonal grid, combine matching items, and work toward completing orders while managing limited space.

The game combines the simplicity of classic merge puzzles with a warm **bakery/café theme**, colorful food items, and satisfying visual feedback.

---

## 🎮 Game Overview

In **HexaBread**, players manage a small bakery-style puzzle board filled with hexagonal cells.

Food items arrive through the game flow and must be strategically placed onto the board. Matching food items can be combined to create upgraded products, allowing players to progress through increasingly valuable recipes.

The challenge comes from balancing:

* 🧩 Limited board space
* 🍞 Food item placement
* 🔄 Merging opportunities
* 📦 Incoming food items
* 🎯 Level objectives
* ⭐ High-score optimization

Every placement matters.

A good move can create a chain of merges and clear valuable space, while a bad move can quickly fill the board and leave the player without useful moves.

---

## ✨ Core Features

### 🔷 Hexagonal Puzzle Grid

The game uses a hexagonal grid rather than a traditional square grid.

Players interact with individual hexagonal cells to place and organize food items.

### 🍞 Food Stacking & Merging

Food items can be placed onto the board and combined with matching items.

As items merge, they progress through different stages of the bakery progression.

```text
Basic Food
    ↓
Tier 2
    ↓
Tier 3
    ↓
Tier 4
    ↓
Premium Food
```

The progression system provides a simple but satisfying gameplay loop while allowing increasingly complex board states.

### 🧠 Strategic Placement

Players need to think about where each item should be placed.

A placement can:

* Create an immediate merge
* Prepare a future merge
* Block another area
* Create a chain reaction
* Free valuable board space

### 🔄 Conveyor / Food Delivery System

Food items are introduced through a delivery/conveyor-style system.

This creates a continuous flow of gameplay and gives players a short window to think about how incoming items should be used.

### ☕ Cozy Café Theme

HexaBread uses a warm bakery/café aesthetic rather than a traditional abstract puzzle presentation.

The visual direction focuses on:

* Warm colors
* Soft lighting
* Stylized 3D food
* Cozy café environments
* Friendly UI
* Playful animations
* Satisfying merge effects

### ⭐ Level Progression

Levels introduce increasingly challenging board configurations and objectives.

Possible objectives include:

* Reach a target score
* Create specific food items
* Complete customer orders
* Perform a certain number of merges
* Clear specific board areas
* Reach a target food tier

---

# 🕹️ Gameplay Loop

The core gameplay loop is designed to be simple to understand but progressively more strategic.

```text
Receive Food
     ↓
Choose Placement
     ↓
Place Food on Grid
     ↓
Match / Merge
     ↓
Create Higher-Tier Food
     ↓
Free Board Space
     ↓
Complete Objectives
     ↓
Earn Rewards
     ↓
Progress to Next Level
```

The goal is to create a gameplay experience that is:

**Easy to learn → Satisfying to play → Difficult to master**

---

# 🧩 Gameplay Mechanics

## Hex Grid

The board is built using hexagonal cells.

Each cell can contain a food item or remain empty.

The hexagonal layout allows multiple neighboring relationships and creates interesting placement possibilities compared to a traditional square grid.

---

## Food Items

Food is represented as individual gameplay objects that can occupy grid cells.

Each food item contains information such as:

* Food type
* Tier
* Visual representation
* Merge state
* Grid position
* Gameplay value

The food progression system allows simple ingredients/items to eventually become more advanced bakery products.

---

## Merging

When compatible food items are placed next to each other, they can be merged into a higher-tier item.

Example:

```text
🍞 + 🍞
   ↓
🥐

🥐 + 🥐
   ↓
🍰
```

The actual food progression can be expanded as the game develops.

---

## Chain Reactions

One of the main goals of the gameplay system is to make merges feel satisfying.

A single merge can potentially create another valid merge, producing a chain reaction.

```text
Place Item
    ↓
Merge
    ↓
New Item Created
    ↓
New Item Matches
    ↓
Second Merge
    ↓
More Space
    ↓
Bonus / Combo
```

Chain reactions provide an additional layer of strategy and reward players for planning ahead.

---

# 🎯 Game Objectives

Levels can use different objective types to keep the gameplay fresh.

### Score Objective

Reach a specific score before the board becomes full.

### Food Objective

Create one or more specific food items.

### Order Objective

Complete a series of bakery/customer orders.

### Merge Objective

Perform a certain number of successful merges.

### Combo Objective

Create a specific number of consecutive merges.

These objectives can be combined to create more interesting levels.

---

# 🏆 Progression

HexaBread is designed around a gradual progression system.

Early levels introduce the basic mechanics:

1. Place food
2. Match food
3. Merge food
4. Create higher-tier food

Later levels introduce additional strategic challenges:

* More complex board layouts
* Limited spaces
* More food types
* Higher objectives
* Special tiles
* Obstacles
* Power-ups
* More difficult orders

The progression is intended to increase complexity without overwhelming new players.

---

# ⚡ Power-Ups

The game can support special tools that help players recover from difficult board situations.

Potential power-ups include:

### 🔄 Swap

Swap the contents of two selected positions.

### 🧹 Clear

Remove a selected food item or group of items.

### 🔀 Shuffle

Rearrange available food items.

### ⬆️ Upgrade

Upgrade a selected food item by one tier.

### 💥 Merge Booster

Trigger or enhance a merge interaction.

Power-ups are intended to provide strategic choices rather than simply acting as emergency buttons.

---

# 🎨 Art Direction

HexaBread follows a **cozy stylized 3D bakery aesthetic**.

The visual direction focuses on making every interaction feel soft, friendly, and rewarding.

### Visual Principles

* Stylized 3D assets
* Rounded shapes
* Warm bakery colors
* Soft shadows
* Gentle lighting
* Minimal visual clutter
* Readable silhouettes
* Juicy animations

The environment is inspired by a cozy café/bakery rather than a traditional puzzle-game interface.

---

# 🎵 Audio Direction

The audio experience is designed to reinforce the cozy and satisfying nature of the game.

Potential audio elements include:

* Soft café ambience
* Food placement sounds
* Merge sounds
* Combo sounds
* Button feedback
* Level completion effects
* Reward sounds
* Subtle background music

Each important gameplay interaction should have clear but non-intrusive audio feedback.

---

# 🛠️ Technology

HexaBread is being developed using **Unity**.

### Tech Stack

| Technology          | Usage                          |
| ------------------- | ------------------------------ |
| Unity               | Game Engine                    |
| C#                  | Gameplay Programming           |
| Unity Input System  | Player Input                   |
| Unity Splines       | Conveyor / Movement Systems    |
| TextMeshPro         | UI Text                        |
| Unity UI            | Interface                      |
| ScriptableObjects   | Game Data / Configuration      |
| HLSL / Shader Graph | Custom Visual Effects          |
| Git                 | Version Control                |
| GitHub              | Source Control & Collaboration |

---

# 🏗️ Project Structure

The Unity project follows a modular structure intended to keep gameplay systems maintainable and scalable.

```text
Assets/
├── Art/
│   ├── Environment/
│   ├── Food/
│   ├── Materials/
│   └── VFX/
│
├── Audio/
│   ├── Music/
│   └── SFX/
│
├── Prefabs/
│   ├── Food/
│   ├── Grid/
│   ├── UI/
│   └── Environment/
│
├── Scenes/
│   ├── Boot/
│   ├── MainMenu/
│   └── Gameplay/
│
├── Scripts/
│   ├── Gameplay/
│   ├── Grid/
│   ├── Food/
│   ├── UI/
│   ├── Managers/
│   └── Utilities/
│
├── ScriptableObjects/
│   ├── Food/
│   ├── Levels/
│   └── Gameplay/
│
├── Shaders/
│
└── UI/
```

The exact folder structure may evolve as development continues.

---

# 🧱 Architecture

The project is being developed with modular gameplay systems to make future features easier to implement.

Some of the primary gameplay components include:

### Grid System

Responsible for:

* Managing board cells
* Detecting available positions
* Tracking occupied cells
* Handling food placement
* Managing neighboring cells

### Food System

Responsible for:

* Food item state
* Food tiers
* Food movement
* Food placement
* Merge behavior

### Merge System

Responsible for:

* Detecting valid merges
* Combining compatible food
* Creating upgraded food
* Handling chain reactions
* Triggering merge feedback

### Level System

Responsible for:

* Loading levels
* Level objectives
* Progress tracking
* Win conditions
* Lose conditions

### UI System

Responsible for:

* Main menu
* Gameplay HUD
* Level progress
* Objectives
* Buttons
* Popups
* Rewards

---

# 📱 Target Platform

The primary target platform is:

* 📱 Android
* 📱 iOS

The game is designed primarily around mobile touch interaction.

---

# 👆 Controls

HexaBread is designed around simple touch controls.

### Tap

Select or interact with a food item.

### Drag

Move a food item toward an available grid position.

### Release

Place the food item into the selected cell.

### UI

Tap buttons to access:

* Power-ups
* Settings
* Level selection
* Rewards
* Other gameplay systems

The control scheme is intentionally minimal so that the gameplay can remain the primary focus.

---

# 🎨 UI Philosophy

The UI is designed around the same cozy visual language as the gameplay.

Important UI principles:

* Large touch targets
* Clear hierarchy
* High readability
* Soft shapes
* Warm colors
* Subtle animations
* Minimal clutter

The interface should feel like an extension of the café environment rather than a separate menu system.

---

# 🚀 Development Goals

The project is currently being developed with a focus on creating a strong and polished core gameplay loop.

### Current Priorities

* [x] Basic Unity project setup
* [x] Hexagonal grid foundation
* [x] Food item system
* [x] Food placement
* [x] Basic merging
* [x] Initial UI
* [ ] Complete level system
* [ ] Complete progression system
* [ ] Final food progression
* [ ] Power-up system
* [ ] Tutorial
* [ ] Audio implementation
* [ ] VFX polish
* [ ] More levels
* [ ] Optimization
* [ ] Mobile testing
* [ ] Final art pass

---

# 🗺️ Roadmap

## Phase 1 — Prototype

* [x] Hex grid
* [x] Food objects
* [x] Food placement
* [x] Merge mechanic
* [x] Basic gameplay loop

## Phase 2 — Core Game

* [ ] Level generation
* [ ] Objectives
* [ ] Win / lose states
* [ ] Food progression
* [ ] Conveyor / delivery system
* [ ] Basic progression

## Phase 3 — Polish

* [ ] Improved animations
* [ ] Merge VFX
* [ ] Audio
* [ ] UI polish
* [ ] Camera polish
* [ ] Environment art
* [ ] Juice / feedback

## Phase 4 — Content

* [ ] Additional levels
* [ ] Additional food items
* [ ] Special tiles
* [ ] Power-ups
* [ ] Challenges
* [ ] Rewards

## Phase 5 — Release

* [ ] Performance optimization
* [ ] Device testing
* [ ] Analytics
* [ ] Monetization
* [ ] Store assets
* [ ] Google Play release
* [ ] App Store release

---

# 🔬 Development Philosophy

HexaBread is being developed with a focus on **game feel** and **clarity**.

The goal is not simply to create a functional puzzle system, but to make every interaction feel satisfying.

Important design principles include:

### Simple Rules

Players should understand the basic gameplay within seconds.

### Meaningful Decisions

Players should have to think about where they place their food.

### Satisfying Feedback

Every merge should feel rewarding through:

* Animation
* Sound
* VFX
* Screen feedback
* Score feedback

### Progressive Complexity

New mechanics should be introduced gradually rather than overwhelming the player.

### Cozy Experience

The game should remain relaxing and approachable even when the puzzle becomes challenging.

---

# 📸 Screenshots & Gameplay

Screenshots and gameplay footage will be added as the project progresses.

### Gameplay

> Coming soon.

### Main Menu

> Coming soon.

### Café Environment

> Coming soon.

### Food Progression

> Coming soon.

---

# 🎥 Development

HexaBread is being developed as an independent game project with a focus on experimenting with:

* Puzzle mechanics
* Mobile game design
* Hexagonal grid systems
* Casual game progression
* Stylized 3D art
* Unity gameplay architecture
* UI/UX
* Game feel and polish

The project also serves as an exploration of building a complete mobile game from prototype through production.

---

# 🤝 Contributing

HexaBread is currently a personal development project.

The repository is primarily intended for development, experimentation, and version control.

If you have suggestions, feedback, or ideas that could improve the gameplay, feel free to open an issue or start a discussion.

---

# 📄 License

The source code and assets in this repository are proprietary unless otherwise stated.

You may **not** redistribute, commercially use, copy, or modify the game's assets, source code, branding, or other proprietary content without explicit permission.

Third-party libraries and packages remain subject to their respective licenses.

---

# 👨‍💻 Developer

**Milan Joshi**

Game Developer & Independent Developer

Portfolio:
https://milanjoshi.framer.website/

---

# 🍞 About HexaBread

HexaBread started as an experiment around a simple question:

> **What if a satisfying hex puzzle was set inside a cozy bakery?**

The project combines strategic hex-based gameplay with the charm of food, cafés, and casual mobile games.

The long-term goal is to turn the prototype into a polished, accessible puzzle game with a strong visual identity and satisfying gameplay loop.

---

## ⭐ Project Status

**Development Status:** 🚧 In Development

HexaBread is actively being prototyped and refined.

More gameplay systems, levels, assets, and polish will be added as development continues.

---

**Made with ❤️ and 🍞 using Unity.**
