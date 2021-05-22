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

    public void GetLoadersToUnload(out List<Loader> toUnload)
    {
      toUnload = new List<Loader>(loaders.Length);
      for (int i = 0; i < loaders.Length; i++)
      {
        if (!loaders[i].CheckFlagRemain()) toUnload.Add(loaders[i]);
      }
    }

  }
}
