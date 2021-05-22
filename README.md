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

This module also uses the following, and therefore they are necessary for extension or improvements:
- ScriptableObjects.
- C#'s generics and interfaces.

If you're not familiar with them, take a look at the **Bibliography** section, or search for resources.


## Usage

There are four important types:

The Asset Locators:
 - BaseArrayAssetLocator
 - BaseDictionaryAssetLocator

 The Scene Locator:
 - BaseSceneLocator

The Asset Manager:
 - BaseAssetManager

### Asset Locators
First of all, inherit these with the types needed in the project. 

They will reference the corresponding assets in Addressables. 

Reference them in the MonoBehaviours that need to instantiate or use these assets.

### Scene Locator
The SceneLocator references a scene and indicates the AssetLocators it will need.

This is where the actual loading/unloading will be called from.

Subscribe to the events to stop/start the gameplay, and show/hide the loading screen.
Check Example1/SceneHandler.cs for an easy usage.

### Asset Manager
The AssetManager keeps the consistency of all the Locators in the game.

It will be called automatically by the AssetLocators.

You only need to extend this and add the references to your custom Asset Locators.

### Example implementations
There are two examples included in the repository. 

**Example 1**. Three map scenes, with different dependencies, and a SceneHandler to switch between them.

**Example 2**. IN DEVELOPMENT.


## Contents
- **AssetLoader module**
    - Interfaces
        - ILoader
        - ISceneLocator
        - IArrayAssetLocator
        - IDictionaryAssetLocator
    - Generic Asset Locators
        - BaseSceneLocator
        - BaseArrayAssetLocator
        - BaseDictionaryAssetLocator
    - Other
        - AssetManager
- **Example 1**. One possible use case: 
    - Three map scenes, with different dependencies, and a SceneHandler to switch between them.
- **Example 2**
    - Another possible use case. IN DEVELOPMENT.
- **Extra tools**
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
