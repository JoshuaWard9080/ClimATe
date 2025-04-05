# ClimATe

ClimATe is a 2D platformer game built with Unity, inspired by the retro classic Ice Climber. The twist is a dynamic Temperature Manager where the environment reacts to climate changes!

---

## Objective

**Reach the top of each level without losing all 9 lives — or being buried by the rising snow.**

---

## Video Demo

**[Link to video showcase of the game](https://youtu.be/8Jas2a7Jvu0)**

---

## [TAs] Individual features commit links
Additonal descriptions of the features and PRs are included in video descriptions and commit messages
### Lucas Slunt

#### Feature 1: Enemies 
  - **[Enemies do Damage PR](https://github.com/JoshuaWard9080/ClimATe/pull/46)**
##### Video Link: **[Enemies video link](https://youtu.be/6iscubNR0Cs)**

#### Feature 2: Modular Levels 
  - **[Modular Levels PR](https://github.com/JoshuaWard9080/ClimATe/pull/16)**
##### Video Link: **[Levels video link](https://youtu.be/19a6PPCgrDw)**

#### Feature 3: Backgrounds 
  - **[Backgrounds PR](https://github.com/JoshuaWard9080/ClimATe/pull/30)**
##### Video Link: **[Backgrounds video link](https://youtu.be/HfqSDD9P6rk)**

---

### Timmi Draper

#### Feature 1: Icicles 
  - **[First Icicles PR](https://github.com/JoshuaWard9080/ClimATe/pull/11)**
  - **[Second Icicles PR](https://github.com/JoshuaWard9080/ClimATe/pull/24)**
##### Video Link: **[Icicle functionality and sound effects video link](https://youtu.be/LJCm6boBRBg)**

#### Feature 2: UI 
  - **[First Main Menu UI PR](https://github.com/JoshuaWard9080/ClimATe/pull/3)**
  - **[Second Main Menu UI PR](https://github.com/JoshuaWard9080/ClimATe/pull/5)**
  - **[Level Complete UI](https://github.com/JoshuaWard9080/ClimATe/pull/6)**
  - **[Victory Scene UI](https://github.com/JoshuaWard9080/ClimATe/pull/27)**
  - **[Score and Stats UI](https://github.com/JoshuaWard9080/ClimATe/pull/38)**
##### Video Link: **[UI video link](https://youtu.be/JHX6elKbxBs)**

#### Feature 3: Piling Snow Death 
  - **[Piling Snow PR](https://github.com/JoshuaWard9080/ClimATe/pull/24)**
##### Video Link: **[Piling snow death video link](https://youtu.be/-N4rrRgr6T0)**

### Joshua Ward

#### Feature 1: Player Movement/Interactions

#### Feature 2: Temperature Changing



---

## Gameplay Overview

- **Platformer Mechanics:**  
  Jump, climb, and explore 4 increasingly difficult levels with environmental hazards and enemies.

- **Dynamic Temperature System:**  
  The current temperature dynamically changes gameplay as you progress:

  - **Cold (Standard):**  
    - Platforms move at standard speed  
    - Winds push players if they stand too long  
    - Snow rises at a standard pace  
    - Birds fly in straight lines  

  - **Freezing:**  
    - Platforms crack faster  
    - Winds are stronger and faster  
    - Snow rises rapidly  
    - Birds can’t fly — they waddle on foot  

  - **Warm:**  
    - Platforms move slower  
    - No wind effects  
    - Snow rises slowly  
    - Yeti enemies no longer deal damage when aggravated  
    - Birds fly in chaotic circular patterns  

- **Interactive World:**  
  Encounter falling icicles, cracking platforms, and icy bricks that all respond to your actions and the climate.

- **Stats & Scoring:**  
  Players are scored across a variety of actions tracked in real-time:

  - **Lives Remaining:** +10 per unused life  
  - **Kills:** Yeti (+10), Bird (+15)  
  - **Bonus Fish:** +5 each — found on top platforms  
  - **Icicles Destroyed:** +5 each  
  - **Blocks Destroyed:** +1 each  
  - **Bonus Time:** If level is finished within 120 seconds, bonus points are awarded  

  The stats tracked across each level are:
  - `totalLivesLost`  
  - `totalTime` (per-level and cumulative)  
  - `totalYetiKills`, `totalBirdKills`, `totalKills`  
  - `blocksDestroyed`, `iciclesDestroyed`, `fishCollected`  
  - `totalPoints` — dynamically calculated and persisted  

---

## Features

- **Temperature Manager**  
  Controls the global temperature and notifies all affected systems (enemies, snow, wind, platforms, etc) via `UnityEvent`.

- **Yeti Enemy AI (`FuzzyEnemy.cs`)**  
  - Uses raycasts to avoid cliffs or walls  
  - Switches direction dynamically  
  - Freezing disables damage behavior  
  - Plays sound when defeated  
  - Contributes to total stats  

- **Bird Enemy AI (`Bird_Enemy.cs`)**  
  - Flies, walks, or drops depending on temperature  
  - Plays walking/flapping audio  
  - Animates hurt states  
  - Integrated with stat system  

- **Cracking Platforms (`CrackingPlatform.cs`)**  
  - Changes color at crack thresholds  
  - Cracks over time when stood on  
  - Plays audio on crack and break  
  - Speed changes based on temperature  

- **Icicles (`Icicle.cs`)**  
  - Fall after being touched  
  - Regenerate after breaking  
  - Can damage or kill the player  
  - Temperature controls fall speed and regen rate  
  - Plays separate sounds for hit and damage  

- **Breakable Blocks (`Block.cs`)**  
  - Can be broken from below by the player  
  - Emits matching colored particles  
  - Plays block-break sound  
  - Adds to score  

- **Bonus Collectibles (`Fruit.cs`)**  
  - Spawns a random sprite  
  - Plays audio on collection  
  - Increments `fishCollected`  

- **Wind Zones (`Wind.cs`)**  
  - Use `AreaEffector2D` to push the player  
  - Strength varies with current temperature  

- **Piling Snow (`PilingSnow.cs`)**  
  - Rises throughout each level  
  - Speed is controlled by temperature  
  - Kills player if it reaches up to their ears

- **Lives Manager (`LivesDisplay.cs`)**  
  - Manages player’s current lives  
  - Plays damage audio  
  - Triggers Game Over screen  
  - Resets visual hearts  

- **Victory Scene Manager (`VictorySceneManager.cs`)**  
  - Scrolls and animates final stats  
  - Displays lives lost, time, kills, points, etc  
  - Plays end scene music  
  - Returns to Main Menu after completion  

---

## Stats Tracking Systems

- **LevelStatsManager** (per-level stats):  
  - Tracks all actions and events for a single level  
  - Automatically starts and stops timers  
  - Calculates total points using:  
    - Lives left  
    - Enemies killed  
    - Time bonus  
    - Fish/blocks/icicles collected  
  - Provides pause/resume functionality  
  - Exposes `CalculateLevelPoints()`  

- **GlobalStatsManager** (cumulative stats):  
  - Aggregates across all levels  
  - `DontDestroyOnLoad` singleton  
  - Tracks all lifetime stats until game reset  
  - Exposes `AddStats()` and `ResetStats()`  
  - Final stats shown on the victory screen  

---

## Player Controls

| Action          | Key         |
|-----------------|-------------|
| Move            | A / D       |
| Jump            | W           |

---

## UI Navigation

| Action              | Key              |
|---------------------|------------------|
| Navigate UI         | W / S            |
| Select / Confirm    | Space / Enter    |
| Pause Menu (in-game)| Escape           |
