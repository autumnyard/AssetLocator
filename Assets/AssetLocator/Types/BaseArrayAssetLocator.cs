using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Collections;

namespace AutumnYard
{
  public abstract class BaseArrayAssetLocator<T> : Loader, IArrayAssetLocator<T>
    where T : UnityEngine.Object
  {

    // Load methods:
    //  - Addressable Label, One by one: Process each assets as soon as it is loaded.
    //  - Addressable Label, In Batch: Wait for all assets to be loaded, and process in batch.


    // TODO: Issue #5: Implement referencing the addressable asset by Folder Reference, instead of Label
    //[SerializeField] protected AddressableReferenceMode referenceMode = AddressableReferenceMode.Label;
    //[SerializeField] protected AssetReferenceT<T> reference = null;

    // TODO: Issue #4: Implement two load modes: One by one, and By batch
    //public enum AddressableLoadMode { OneByOne, InBatch }
    //[SerializeField] private AddressableLoadMode loadMode = AddressableLoadMode.OneByOne;

    [SerializeField] private AssetLabelReference label = null;
    private AsyncOperationHandle<IList<T>> asyncHandle;
    protected List<T> data = null;


    private void OnEnable()
    {
      IsLoaded = false;
      if (data == null) return;

      data.Clear();
      data = null;
    }

    private void OnValidate()
    {
      Assert.AreNotEqual<string>(label.labelString, string.Empty, $"Invalid AssetLabelReference in {name}");
      Assert.AreNotEqual<string>(label.labelString, "default", $"Invalid AssetLabelReference in {name}");
    }

    #region ILoader

    public override event Action OnUnloadingBegin;
    public override event Action OnUnloadingFinish;
    public override event Action OnLoadingBegin;
    public override event Action OnLoadingFinish;


    public override IEnumerator Load()
    {
      if (data != null && data.Count > 0)
      {
        Log($"Already loaded: {name}");
        yield break;
      }

      Logger.Log($"  ... <b>Yes!</b> <color=green>Load</color> {name}", Logger.Type.Example1);

      if (string.IsNullOrEmpty(label.labelString)) throw new NullReferenceException($"Invalid AssetLabelReference in {name}");

      Log($"Begin loading {name}...");
      OnLoadingBegin?.Invoke();

      data = new List<T>();
      asyncHandle = Addressables.LoadAssetsAsync<T>(label, HandleAssetLoad);
      yield return asyncHandle;

      Log($"  ... finished!");
      OnLoadingFinish?.Invoke();
      IsLoaded = true;

      void HandleAssetLoad(T asset) => data.Add(asset);
    }

    public override IEnumerator Unload()
    {
      if (data == null || data.Count == 0)
      {
        Log($"No need to unload: {name}");
        yield break;
      }

      Logger.Log($"  ... <b>Yes!</b> <color=red>Unload</color> {name}", Logger.Type.Example1);

      // data.Clear(); // Not clearing a List preserves the allocated space for longer
      data = null;
      Addressables.Release(asyncHandle);
      Log($"Unloaded {name}!");
    }

    #endregion // ILoader


    #region IArrayAssetLocator

    public T this[int which] => Get(which);

    public T Get(int which)
    {
      if (which >= data.Count)
      {
        LogError($"Trying to get position {which} when size is {data.Count}");
        return null;
      }
      return data[which - 1];
    }

    #endregion // IArrayAssetLocator


    #region Load methods

    /// <summary> Load asset by asset, and call OnComplete for each one </summary>
    private void LoadMethod1(int length)
    {
      if (label == null) throw new NullReferenceException($"No has seleccionado un AssetLabelReference en {name}");

      data = new List<T>(length);
      Addressables.LoadAssetsAsync<T>(label, HandleSpriteLoaded);

      void HandleSpriteLoaded(T sprite)
      {
        Log($"Load: {sprite.name}");
        data.Add(sprite);
      }
    }

    /// <summary> Load all the assets in one batch, and call OnComplete when all were loaded </summary>
    private void LoadMethod2(int length)
    {
      if (label == null) throw new NullReferenceException($"No has seleccionado un AssetLabelReference en {name}");


      data = new List<T>(length);
      asyncHandle = Addressables.LoadAssetsAsync<T>(label, null);
      asyncHandle.Completed += OnComplete;


      void OnComplete(AsyncOperationHandle<IList<T>> objects)
      {
        Assert.AreEqual(length - 1, objects.Result.Count); // omit None

        asyncHandle.Completed -= OnComplete;
        if (objects.Status != AsyncOperationStatus.Succeeded) throw new Exception($"LA OPERACION HA FALLADO");
        foreach (var go in objects.Result)
        {
          Log($"Loading sprite {go.name.ToLower()}");
          data.Add(go);
        }
        //areItemImagesLoaded = true;
        Log($"Finished loading! {name}");
      }
    }

    #endregion


    protected void Log(string text)
    {
      Logger.Log(text, Logger.Type.ArrayAssetLocator);
    }

    protected void LogError(string text)
    {
      Logger.LogError(text, Logger.Type.ArrayAssetLocator);
    }

  }
}
