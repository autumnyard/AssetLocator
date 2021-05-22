# AssetLocator


## Description
**AssetLocator** is a module to manage the assets and their dependencies in a Unity project. 
It provides a facade to the Addressables system, and an intuitive way to manage the assets in the project.


## Goal
- Provide a facade to work with Addressables in an easy and intuitive way.
- Enforce a healthy workflow on the project of keeping track of the assets and their dependencies. This makes memory management and asset dependency a conscious part of the development cycle.
- It should allow for a seamless editor workflow.
- Easy set-up. No non-sense complexity. As minimal and to-the-point as possible.
- Flexible for extension in as varied use cases as possible.
- MAYBE: Possible to use by non-programmers?


## Considerations
I'm assuming familiarity with the following concepts:
- Unity Addresables system.

This module also uses the following, but they are not necessary for basic usage. However, they are necessary for extension:
- ScriptableObjects.
- C#'s generics and interfaces.

## Usage

### Asset Locators
 - What it is: 
   - References a collection of assets in Addressables. 
 - Classes:
   - BaseArrayAssetLocator
   - BaseDictionaryAssetLocator
 - What it does:
   - Load/unload the referenced Addressables.
 - What should you do:
   - 1) Inherit these with the types needed in the project.   
   - 2) Reference them in the MonoBehaviours that need to instantiate or use these assets.

### Scene Locator
 - What it is:
   - References a scene.
   - References a collection of AssetLocators it will need.
 - Classes:
   - BaseSceneLocator
 - What it does: 
   - Load/unload the referenced scene
   - Mark its Locators with the flag "Remain loaded".
 - What should you do:
   - 3) Check Example1/SceneHandler.cs for easy usage of Load/Unload/CheckFlagRemain.
   - 4) Subscribe to the events to stop/start the gameplay, and show/hide the loading screen.

### Asset Manager
 - What it is: 
   - An index of all the AssetLocators in the game.
 - Classes:
   - BaseAssetManager
 - What it does:
   - Checks all the Locators for the ones that should be Loaded/Unloaded.
 - What should you do:
   - 5) Reference all the AssetLocators in the project.
   - 6) Call CheckFlags for it to do it's magic. Again, check Example1/SceneHandler.cs to see when to make the call.

### Order
1) Check the new map's dependencies, and set the FlagRemains accordingly.
2) Unload the old map, so there are no active references to any assets.
3) Call AssetManager to trigger Locators loading or unloading as needed.
4) Load the new map.

### Example implementations
There are two examples included in the repository. 

**Example 1**. 
 - Three map scenes, with different dependencies, and a SceneHandler to switch between them.
 - The most important thing to see here is the **SceneHandler** and the three methods that change map.

**Example 2**. **IN DEVELOPMENT.**
 - Creates a hierarchy of SceneLocators: Context and Map. Context is over Map.
 - An instance of both of them will have to be active at the same time.
 - Only one Context, and one Map, can be active at any given time.
   - Context: GameWalking, GameTravel, MainMenu.
   - Map: Map1, Map2, Map3, Overmap.
 - When you are in GameWalking, you can change between Map1, 2 and 3.
 - However, when you change the Context to GameTravel, Overmap will be loaded, and any other Map will be unloaded.


## Contents
- **AssetLoader**
    - BaseAssetManager
    - Interfaces
        - ILoader
        - ISceneLocator
        - IArrayAssetLocator
        - IDictionaryAssetLocator
    - Generic Asset Locators
        - BaseSceneLocator
        - BaseArrayAssetLocator
        - BaseDictionaryAssetLocator
- **Example1**. One possible use case: 
    - Three map scenes, with different dependencies, and a SceneHandler to switch between them.
- **Example2**
    - Another possible use case. IN DEVELOPMENT.
- **Tools**
    - Some extra helper tools I use during development of this module.
    - Unrelated to the module, and therefore unnecessary for it to work. Feel free to ignore them.


## Bibliography
 * Unity Addresables system
 * ScriptableObjects
 * C# interface and generics


## About & License
Made by **Autumn Yard (Pablo de la Ossa)**
Find me at: 
 * http://autumnyard.com
 * http://pablodelaossa.es

AssetLocator is licensed under the GPL version 3 License. For more information check the LICENSE.md file or the gnu website.
