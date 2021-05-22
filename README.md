# AssetLocator

Unity module to manage asset references and dependencies between scenes using Addressables.

### Goal
- Provide a facade to work with Addressables in an easy and intuitive way.
- Force the developers to keep track of all the assets and their dependencies, making memory management part of the development cycle.
- Allowing for a seamless editor workflow.

### Considerations
I'm assuming familiarity with the following concepts:
- Unity Addresables system.
- ScriptableObjects.
- C#'s generics and interfaces.

If you're not familiar with them, take a look at the **Bibliography** section, or search for resources.

### Contents
- **AssetLoader module**
    - Interfaces
        - ILoader
        - IMapLocator
        - IArrayAssetLocator
        - IDictionaryAssetLocator
    - Generic Asset Locators
        - BaseMapLocator
        - BaseArrayAssetLocator
        - BaseDictionaryAssetLocator
    - Other
        - AssetManager
- **Example 1**
    - One possible use case: Three maps, with different dependencies, and a SceneHandler to switch between them.
- **Example 2**
    - Another possible use case. IN DEVELOPMENT.
- **Extra tools**
    - Some extra helper tools I use during development of this module.
    - Unrelated to the module, and therefore unnecessary for it to work. Feel free to ignore them.

### Usage
Extending the base types with the ones needed in the project.

### Bibliography
 * Unity Addresables system
 * ScriptableObjects
 * C# interface and generics

## About
Made by **Autumn Yard (Pablo de la Ossa)**
Find me at: 
 * http://autumnyard.com
 * http://pablodelaossa.es