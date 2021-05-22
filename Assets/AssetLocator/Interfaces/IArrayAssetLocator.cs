using System;
using System.Collections;

namespace AutumnYard
{
  public interface IArrayAssetLocator<T> where T : UnityEngine.Object
  {
    IEnumerator Load<TEnum>() where TEnum : struct, Enum;
    IEnumerator Unload();
    T this[int which] { get; }
    T Get(int which);
  }
}
