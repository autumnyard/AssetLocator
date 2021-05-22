using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutumnYard.Example2
{
  [CreateAssetMenu(fileName = "Context Configuration", menuName = "Autumn Yard/Context Configuration", order = 100)]
  public class ContextConfiguration : ScriptableObject/*, ILoader*/
  {
    [Header("Assets")]
    [Header("Generic dictionaries, accessible with string")]
    [SerializeField] private SpriteDictionaryAssetLocator[] spriteLocators;
    [SerializeField] private AudioClipDictionaryAssetLocator[] audioClipLocators;
    [SerializeField] private PrefabDictionaryAssetLocator[] prefabLocators;

    [Header("Context")]
    //[SerializeField, EnumToggleButtons] private Context context;
    [SerializeField] private string scene;




    public bool IsLoaded => true;

    public event Action OnUnloadingBegin;
    public event Action OnUnloadingFinish;
    public event Action OnLoadingBegin;
    public event Action OnLoadingFinish;

    public IEnumerator LoadDependenciesOnly()
    {
      // Dependencies
      Debug.Log($" +    Load Dependencies");
      if (spriteLocators != null)
      {
        for (int i = 0; i < spriteLocators.Length; i++)
        {
          yield return spriteLocators[i].Load();
        }
      }

      if (audioClipLocators != null)
      {
        for (int i = 0; i < audioClipLocators.Length; i++)
        {
          yield return audioClipLocators[i].Load();
        }
      }
      if (prefabLocators != null)
      {
        for (int i = 0; i < prefabLocators.Length; i++)
        {
          yield return prefabLocators[i].Load();
        }
      }
    }

    public IEnumerator Load()
    {
      Debug.Log($" + Load context {scene}");

      if (string.IsNullOrEmpty(scene))
      {
        yield return null;
      }
      else
      {
        yield return LoadDependenciesOnly();

        Debug.Log($" +    Load Context");
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
      }
    }

    public IEnumerator Unload()
    {
      Debug.Log($" + Unload context {scene}");
      if (string.IsNullOrEmpty(scene))
      {
        yield return null;
      }
      else
      {
        yield return SceneManager.UnloadSceneAsync(scene);
      }
    }

    public IEnumerator LoadAll()
    {
      throw new NotImplementedException();
    }

    public IEnumerator LoadMapOnly()
    {
      throw new NotImplementedException();
    }
  }
}