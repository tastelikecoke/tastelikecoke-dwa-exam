# tastelikecoke-dwa-exam

## How To Run
1. Install Unity 2020.3.31f1, optionally with PC/Mac/Linux Build Tools.
2. Run Assets/Scenes/Main.scene, or build the game directly.

## Third Party Plugins
- no third party plugins.  All assets are in Unity or generated using Unity or MSPaint.

## Controls
- Mouse and Keyboard (WASD, Mouse curose, Left Mouse Button) implemented
- Controller/gamepad (Left stick, Right stick, Right shoulder button, Right trigger) implemented

## How To Create a New Enemy
- Start by creating a new prefab in `Assets/Prefabs/Enemy Prefabs`.

### Enemy from Scratch
- Start from an empty object prefab. The enemy needs these components:

    - Collider (any)
    - Character Controller
    - Enemy Controller
    - Health
    - Enemy Config Leader

- Optionally add a mesh for the enemy so it is visible.

- Add optional attack components. These are the optional components:

    - Enemy Attack (ranged attack)
    - Enemy Melee

### Enemy from Prefab
- Duplicate one of the prefabs in `Assets/Prefabs/Enemy Prefabs`.

- Using the Enemy Config Loader's Config File name property, set the config it uses at runtime.

    - Optionally create an EnemyConfig in `Resources/Config` for it to specifically use.

- Set the Enemy to spawn by putting the Enemy prefab reference to Enemies > Enemy Spawner > Enemy Prefab Types

    - Alternatively, just drop the prefab to the game scene.  The enemy's tracking will not work, but it won't cause errors.