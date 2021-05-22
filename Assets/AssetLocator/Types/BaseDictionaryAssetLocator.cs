using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using UnityEngine.Assertions;

namespace AutumnYard
{
  public abstract class BaseDictionaryAssetLocator<T> : Loader, IDictionaryAssetLocator<T>
    where T : UnityEngine.Object
  {

    // Load methods:
    //  - Addressables Reference: Load the asset by direct addressable reference.
    //  - Addressable Label, One by one: Process each assets as soon as it is loaded.
    //  - Addressable Label, In Batch: Wait for all assets to be loaded, and process in batch.
    //
    // Best configurations:
    //  - For single asset: Addressables, Reference.
    //  - For multiple assets: Addressables, Label, OneByOne.


    // TODO: Issue #5: Implement referencing the addressable asset by Folder Reference, instead of Label
    //[SerializeField] protected AddressableReferenceMode referenceMode = AddressableReferenceMode.Label;
    //[SerializeField] protected AssetReferenceT<T> reference = null;

    // TODO: Issue #4: Implement two load modes: One by one, and By batch
    //public enum AddressableLoadMode { OneByOne, InBatch }
    //[SerializeField] private AddressableLoadMode loadMode = AddressableLoadMode.OneByOne;

    [SerializeField] private AssetLabelReference label = null;
    private AsyncOperationHandle<IList<T>> asyncHandle;
    protected Dictionary<string, T> data = null;



    private void OnEnable()
    {
      if (data == null) return;

      data.Clear();
      data = null;
      IsLoaded = false;
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

      if (string.IsNullOrEmpty(label.labelString)) throw new NullReferenceException($"Invalid AssetLabelReference in {name}");

      Log($"Begin loading {name}...");
      OnLoadingBegin?.Invoke();

      data = new Dictionary<string, T>();
      asyncHandle = Addressables.LoadAssetsAsync<T>(label, HandleAssetLoad);
      yield return asyncHandle;

      Log($"  ... finished!");
      OnLoadingFinish?.Invoke();
      IsLoaded = true;

      void HandleAssetLoad(T asset) => data.Add(asset.name.ToLower(), asset);
    }

    public override IEnumerator Unload()
    {
      // TODO: Issue #6: Unload assets
      throw new NotImplementedException("Unloading assets");
    }

    #endregion // ILoader


    #region IDictionaryAssetLocator

    public T this[string which] => Get(which);

    public T Get(string which)
    {
      string key = which.ToLower();
      if (!data.ContainsKey(key))
      {
        LogError($"Doesn't contain key {key}");
        return null;
      }
      return data[key];
    }

    #endregion // IDictionaryAssetLocator


    #region Load methods

    /// <summary> Load asset by asset, and call OnComplete for each one </summary>
    private void LoadMethod1()
    {
      if (label == null) throw new NullReferenceException($"No has seleccionado un AssetLabelReference en {name}");

      data = new Dictionary<string, T>();
      Addressables.LoadAssetsAsync<T>(label, HandleSpriteLoaded);

      void HandleSpriteLoaded(T sprite)
      {
        Log($"Load: {sprite.name}");
        data.Add(sprite.name.ToLower(), sprite);
      }
    }

    /// <summary> Load all the assets in one batch, and call OnComplete when all were loaded </summary>
    private void LoadMethod2()
    {
      if (label == null) throw new NullReferenceException($"No has seleccionado un AssetLabelReference en {name}");

      data = new Dictionary<string, T>();
      asyncHandle = Addressables.LoadAssetsAsync<T>(label, null);
      asyncHandle.Completed += OnComplete;


      void OnComplete(AsyncOperationHandle<IList<T>> objects)
      {
        asyncHandle.Completed -= OnComplete;
        if (objects.Status != AsyncOperationStatus.Succeeded) throw new Exception($"LA OPERACION HA FALLADO");
        foreach (var go in objects.Result)
        {
          Log($"Loading sprite {go.name.ToLower()}");
          data.Add(go.name.ToLower(), go);
        }
        //areItemImagesLoaded = true;
        Log($"Finished loading! {name}");
      }
    }

    #endregion


    protected void Log(string text)
    {
      Logger.Log(text, Logger.Type.DictAssetLocator);
    }

    protected void LogError(string text)
    {
      Logger.LogError(text, Logger.Type.DictAssetLocator);
    }
  }
}
