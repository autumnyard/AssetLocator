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
 - What it is: An AssetLocator references the corresponding assets in Addressables. 
 - Classes:
   - BaseArrayAssetLocator
   - BaseDictionaryAssetLocator
 - What it does:
   - Load/unload the referenced Addressables.
 - What should you do:
   - 1) Inherit these with the types needed in the project.   
   - 2) Reference them in the MonoBehaviours that need to instantiate or use these assets.

### Scene Locator
 - What it is: The SceneLocator references a scene and indicates the AssetLocators it will need.
 - Classes:
   - BaseSceneLocator
 - What it does: 
   - Load/unload the referenced scene
   - Mark its Locators with the flag "Remain loaded".
 - What should you do:
   - 3) Check Example1/SceneHandler.cs for an easy usage.
   - 4) Subscribe to the events to stop/start the gameplay, and show/hide the loading screen.

### Asset Manager
 - What it is: An index of all the AssetLocators in the game.
 - Classes:
   - BaseAssetManager
 - What it does: 
   - The AssetManager keeps the consistency of all the Locators in the game.
   - What locators should be loaded and unloaded? It decides.
 - What should you do:
   - 5) You only need to extend this and add the references to your custom Asset Locators.
   - 6) Reference all the AssetLocators in the project.

### Example implementations
There are two examples included in the repository. 

**Example 1**. 
 - Three map scenes, with different dependencies, and a SceneHandler to switch between them.
 - The most important thing to see here is the **SceneHandler** and the three methods that change map.

**Example 2**. 
 - IN DEVELOPMENT.


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
