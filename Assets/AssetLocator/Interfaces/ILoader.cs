using System;
using System.Collections;

namespace AutumnYard
{
  public interface ILoader
  {
    event Action OnUnloadingBegin;
    event Action OnUnloadingFinish;
    event Action OnLoadingBegin;
    event Action OnLoadingFinish;

    bool IsLoaded { get; }

    IEnumerator Load();
    IEnumerator Unload();
  }
}
