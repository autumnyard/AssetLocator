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
    public virtual event Action OnUnloadingFinish;
    public abstract event Action OnLoadingBegin;
    public virtual event Action OnLoadingFinish;

    public void SetFlagRemain() => flagRemain = true;

    public bool CheckFlagRemain() => flagRemain;

    public abstract IEnumerator Load();

    public abstract IEnumerator Unload();
  }
}
