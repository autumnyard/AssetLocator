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
    [Header("Map")]
    // TODO: Issue #1.
    // [SerializeField] private string map; 
    [SerializeField] private AssetReference sceneRef;

    private AsyncOperationHandle<SceneInstance> asyncOp;


    #region Map Locator

    protected IEnumerator CheckDependency(ILoader which)
    {
      if (which != null)
      {
        which.SetFlagRemain();
        yield return which.Load();
      }
      else
      {
        // TODO: Issue #2. This dependency should be unloaded.
      }
    }

    protected IEnumerator CheckDependency<T>(IArrayAssetLocator<T> which)
      where T : UnityEngine.Object
    {
      if (which != null)
      {
        yield return which.Load();
      }
      else
      {
        // TODO: Issue #2. This dependency should be unloaded.
      }
    }

    protected abstract IEnumerator LoadDependenciesOnly();

    private IEnumerator LoadMapOnly()
    {
      Log($" ... loading scene {sceneRef.SubObjectName}...");
      {
        asyncOp = Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Additive, false);
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
      yield return LoadDependenciesOnly();
      yield return LoadMapOnly();
    }

    public override IEnumerator Unload()
    {
      Log($"Unloading map {name}...");
      OnUnloadingBegin?.Invoke();

      {
        Log($"Begin unloading map {sceneRef.SubObjectName}...");
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