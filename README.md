# AssetLocator

## Description
**AssetLocator** works as the basis for a module that manages the assets that are generated dynamically on a project, and their dependencies. It provides a facade to the Addressables system, and a workflow using explicit references and dependencies with ScriptableObjects called Locators.

## Goal
- Provide a facade to work with Addressables in an easy and intuitive way.
- Force the developers to keep track of all the assets and their dependencies, making memory management a conscious part of the development cycle.
- Allowing for a seamless editor workflow.

## Considerations
I'm assuming familiarity with the following concepts:
- Unity Addresables system.
- ScriptableObjects.
- C#'s generics and interfaces.

If you're not familiar with them, take a look at the **Bibliography** section, or search for resources.

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

## Usage
Extending the base types with the ones needed in the project.
Check **Example 1** and **Example 2** for ideas.

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