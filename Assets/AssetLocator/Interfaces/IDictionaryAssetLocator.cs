using System.Collections;

namespace AutumnYard
{
  public interface IDictionaryAssetLocator<T> where T : UnityEngine.Object
  {
    IEnumerator Load();
    IEnumerator Unload();
    T this[string which] { get; }
    T Get(string which);
  }
}
