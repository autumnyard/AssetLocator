using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AutumnYard
{
  public abstract class BaseSceneLocator : Loader, ISceneLocator
  {
    [Header("Scene")]
    // TODO: Issue #1.
    // [SerializeField] private string map; 
    [SerializeField] private AssetReference sceneReference;
    [SerializeField] private Loader[] dependencies;

    private AsyncOperationHandle<SceneInstance> asyncOp;


    #region Map Locator

    public void CheckDependenciesAndTriggerRemainFlags()
    {
      for (int i = 0; i < dependencies.Length; i++)
      {
        TriggerRemainFlag(dependencies[i]);
      }
    }

    protected virtual void TriggerRemainFlag(Loader which)
    {
      if (which != null)
      {
        which.TriggerFlagRemain();
      }
    }


    [Obsolete("LoadDependenciesOnly is deprecated, please use AssetManager.CheckFlagRemains instead. Dependencies should be Loaded/Unloaded on the AssetManager, not here.")]
    protected IEnumerator LoadDependenciesOnly()
    {
      for (int i = 0; i < dependencies.Length; i++)
      {
        yield return LoadDependency(dependencies[i]);
      }
    }

    protected IEnumerator LoadDependency(Loader which)
    {
      if (which != null)
      {
        yield return which.Load();
      }
    }


    private IEnumerator LoadMapOnly()
    {
      Log($" ... loading scene {sceneReference.SubObjectName}...");
      {
        asyncOp = Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive, false);
        yield return asyncOp;
      }
      Log($" ... finished with result: {asyncOp.Status}");

      IsLoaded = true;
      OnLoadingFinish?.Invoke();
    }

    public virtual IEnumerator Activate()
    {
      Log($"Activating map {name}!");
      yield return asyncOp.Result.ActivateAsync();
    }

    #endregion


    #region ILoader

    public override event Action OnUnloadingBegin;
    public override event Action OnUnloadingFinish;
    public override event Action OnLoadingBegin;
    public override event Action OnLoadingFinish;


    public override IEnumerator Load()
    {
      Log($"Begin loading map {name}...");
      OnLoadingBegin?.Invoke();
      yield return LoadMapOnly();
    }

    public override IEnumerator Unload()
    {
      Log($"Unloading map {name}...");
      OnUnloadingBegin?.Invoke();

      {
        Log($"Begin unloading map {sceneReference.SubObjectName}...");
        // TODO: Issue #1.
        // if (sceneRef.Asset == null)
        // {
        //   Log($" ... it was already present when you pressed Play. Unload normally {map}");
        //   yield return SceneManager.UnloadSceneAsync(map);
        // }
        // else
        // {
        asyncOp = Addressables.UnloadSceneAsync(asyncOp, true);
        yield return asyncOp;
        // Addressables already takes care of all the automatic dependencies
        Log($" ... finished!");
        // }
      }

      IsLoaded = false;
      OnUnloadingFinish?.Invoke();
    }

    #endregion // ILoader


    protected void Log(string text)
    {
      Logger.Log(text, this);
    }

    protected void LogError(string text)
    {
      Debug.LogError(text, this);
    }

  }
}