using System;
using System.Collections;

namespace AutumnYard
{
  public interface ILoader
  {
    bool IsLoaded { get; }

    event Action OnUnloadingBegin;
    event Action OnUnloadingFinish;
    event Action OnLoadingBegin;
    event Action OnLoadingFinish;

    void TriggerFlagRemain();
    bool CheckFlagRemainAndClear();

    IEnumerator Load();
    IEnumerator Unload();
  }
}
