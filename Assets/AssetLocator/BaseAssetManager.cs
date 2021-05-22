using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard
{
  [CreateAssetMenu(fileName = "Asset Manager", menuName = "Autumn Yard/Base Asset Manager", order = 100)]
  public class BaseAssetManager : ScriptableObject
  {
    [SerializeField] private Loader[] loaders;

    /// <summary>
    /// Calls Load/Unload on the assets using their RemainFlag flags.
    /// </summary>
    public IEnumerator CheckFlagRemains()
    {
      Logger.EmptyLine(Logger.Type.Example1);

      for (int i = 0; i < loaders.Length; i++)
      {
        Logger.Log($"Should we do something with {loaders[i].name}?", Logger.Type.Example1);

        if (loaders[i].CheckFlagRemainAndClear())
        {
          yield return loaders[i].Load();
        }
        else
        {
          yield return loaders[i].Unload();
        }
      }
    }

    /// <summary>
    /// Returns a list of loaders to load and unload. <b>Only returns the lists</b>
    /// </summary>
    /// <param name="toUnload">The List of managers that should be unloaded.</param>
    [Obsolete("GetLoadersToUnload is deprecated, please use CheckFlagRemains instead.")]
    public void GetLoadersToUnload(out List<Loader> toLoad, out List<Loader> toUnload)
    {
      toLoad = new List<Loader>(loaders.Length);
      toUnload = new List<Loader>(loaders.Length);

      for (int i = 0; i < loaders.Length; i++)
      {
        if (loaders[i].CheckFlagRemainAndClear())
        {
          toLoad.Add(loaders[i]);
        }
        else
        {
          toUnload.Add(loaders[i]);
        }
      }
    }

    protected void Log(string text) => Logger.Log(text, Logger.Type.AssetManager);
    protected void LogError(string text) => Logger.LogError(text, Logger.Type.AssetManager);

  }
}
