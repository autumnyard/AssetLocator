using System;
using System.Collections;
using UnityEngine;

namespace AutumnYard
{
  public abstract class Loader : ScriptableObject, ILoader
  {
    public bool IsLoaded { get; protected set; } = false;

    protected bool flagRemain = false;

    public abstract event Action OnUnloadingBegin;
    public abstract event Action OnUnloadingFinish;
    public abstract event Action OnLoadingBegin;
    public abstract event Action OnLoadingFinish;

    public void TriggerFlagRemain()
    {
      flagRemain = true;
    }

    public bool CheckFlagRemainAndClear()
    {
      bool temp = flagRemain;
      flagRemain = false;
      return temp;
    }

    public abstract IEnumerator Load();

    public abstract IEnumerator Unload();

  }
}
