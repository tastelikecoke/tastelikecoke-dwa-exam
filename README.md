# tastelikecoke-dwa-exam

# How To Run
1. Install Unity 2020.3.31f1, optionally with PC/Mac/Linux Build Tools.
2. Run Assets/Scenes/Main.scene, or build the game directly.

# Third Party Plugins
- no third party plugins.  All assets are in Unity or generated using Unity or MSPaint.

# How To Create a New Enemy
- Start by creating a new prefab in Assets/Prefabs/EnemyPrefabs.

## Enemy from Scratch
1. Start from an empty object. The enemy needs these components:
  - Collider (any)
  - Character Controller
  - Enemy Controller
  - Health
  - Enemy Config Leader
2. Add optional attack components. These are the optional components:
  - Enemy Attack (ranged attack)
  - Enemy Melee

3. Using the Enemy Config Loader's Config File name property, set the config it uses at runtime.

4. Set the Enemy to spawn by putting the Enemy prefab reference to Enemies > Enemy Spawner > Enemy Prefab Types
  - Alternatively, just drop the prefab to the game scene.  The enemy's tracking will not work, but it won't cause errors.