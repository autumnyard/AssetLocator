using UnityEngine;

namespace AutumnYard.Example2
{
  [CreateAssetMenu(fileName = "Prefab Locator (dictionary)", menuName = "Autumn Yard/Prefab Locator (with dictionary)", order = 100)]
  public class PrefabDictionaryAssetLocator : BaseDictionaryAssetLocator<GameObject>
  {
    public GameObject Generate(string which, Transform transform, Transform sibling = null)
    {
      return GameObject.Instantiate(this[which], transform.transform.position, transform.transform.rotation, sibling);
    }
  }
}
