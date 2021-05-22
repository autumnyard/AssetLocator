using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard
{
  [CreateAssetMenu(fileName = "Asset Manager", menuName = "Autumn Yard/Base Asset Manager", order = 100)]
  public class BaseAssetManager : ScriptableObject
  {
    [SerializeField] private Loader[] loaders;

    public void GetLoadersToUnload(out List<Loader> toUnload)
    {
      toUnload = new List<Loader>(loaders.Length);
      for (int i = 0; i < loaders.Length; i++)
      {
        if (!loaders[i].CheckFlagRemainAndClear())
        {
          toUnload.Add(loaders[i]);
        }
      }
    }

    public IEnumerator CheckFlagRemains()
    {
      for (int i = 0; i < loaders.Length; i++)
      {
        Log($"Check locator {loaders[i].name}");
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

    protected void Log(string text) => Logger.Log(text, Logger.Type.AssetManager);
    protected void LogError(string text) => Logger.LogError(text, Logger.Type.AssetManager);

  }
}
