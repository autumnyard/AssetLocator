using System;
using System.Collections;

namespace AutumnYard
{
  public interface IArrayAssetLocator<T> where T : UnityEngine.Object
  {
    // IEnumerator Load(int length);
    IEnumerator Load();
    IEnumerator Unload();
    T this[int which] { get; }
    T Get(int which);
  }
}
