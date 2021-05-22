using UnityEngine;

namespace AutumnYard.Example1
{
  [CreateAssetMenu(fileName = "Prefab Locator (array)", menuName = "Autumn Yard/Prefab Locator (with array)", order = 100)]
  public class PrefabArrayAssetLocator : BaseArrayAssetLocator<GameObject>
  {
    public GameObject Generate(int which, Transform transform, Transform sibling = null)
    {
      return GameObject.Instantiate(this[which], transform.transform.position, transform.transform.rotation, sibling);
    }
  }
}
