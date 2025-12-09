# Planet-Crusher

![ezgif-2de6ae61b865a00f](https://github.com/user-attachments/assets/12c74de6-7e57-4c34-bb49-68407bbfad7c)

## Description

Planet Crusher is a game where you control a spaceship flying through space.

There are multiple planetary bodies that you can fly into to watch them explode.

The game is made in Unity 6000.2.10f1.

The background soundtrack has been made by my wonderfully talented friend [Iris](https://www.instagram.com/iris_mealor_olsen/)

The game has used assets from [LowpolySpaceshipPack](https://assetstore.unity.com/packages/3d/vehicles/space/lowpolyspaceshippack-196496) and [Real Stars Skybox Lite](https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-lite-116333)

## Main Game Elements

- Planetary bodes that have different properties such as color, emission, mass, etc
  - All planetary bodies exert force on one another
  - All planetary bodies can be hit with the player spaceship, and will then explode after one second.
  - The position of the planetary bodies are semi-randomly assigned at start
- Player Spaceship
  - You can move the spaceship around with WASD
  - You can rotate the orientation of the spaceship by pressing Q and E
  - You can propulse and accelerate your ship forward by pressing shift
  - You can brake and deaccelerate your spaceship by pressing space
- UI system
  - Tells you how much mass you have destroyed in total by exploding plantes
  - Tells you your current velocity
 
## Running it

- Download Unity >= 6000.2.10f1
- Clone or Download the project
- Open project in Unity Editor
- Open Main Scene
  - You can choose to play the game directly in the Unity Editor
  - You can also create a build by:
    - Go to File -> Build Profiles
    - Choose platform *(only Windows has been tested and verified as working)*
    - Choose Scenes/Main Scene as the only Scene in Scene List
    - Make build

The game requires a computer with a keyboard

## Project Parts

### Scripts

- PlanetaryBody - Scriptable object used for holding planetary body data and initialization/behavior logic
- PlanetaryBodyCollider - Component for PlanetaryBody scriptable object which holds collider logic
- PlanetaryBodiesMovement - Holds logic for assigning initial positions and calculating movement for planetary bodies 
- PlanetaryManager - Holds logic for managing all planetary bodies. Inherits from PlanetaryBodiesMovement
- PlayerController - Manages player input and player movement
- SoundManager - Controls explosion sound
- VelocityText - Shows current velocity on UI
- MassDestroyedText - Shwos total destroyed mass on UI

### Assets

- Spaceship4_2.fbx as model and ColorPalette.mat as material for spaceship from [LowpolySpaceshipPack](https://assetstore.unity.com/packages/3d/vehicles/space/lowpolyspaceshippack-196496)
- StartSkybox04.mat as Skybox Material from [Real Stars Skybox Lite](https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-lite-116333)

## Timetable

| Task       | Time it took (in hours)|
|:------------- |:-------------|
| Setting up planetary system logic     | 4 | 
| Finding assets      | 1.5      | 
| Creating player controls | 4      | 
| Making procedural random material settings for planetary bodies | 3      | 
| Tweaking physics logic | 3      | 
| Playtesting and tweaking variables | 3      | 
| Setting up sound | 2.5      | 
| Changing planetary body material logic so materials work in build | 4      | 
| Documentation and comments in code | 1      | 
| Making UI | 2      | 
| Making ReadME and uploading to Unity | 2      | 
| **Total** | **30**      | 







