using System.Collections;
using UnityEngine;

namespace AutumnYard.Example1
{
  [CreateAssetMenu(fileName = "Example1 Map Locator", menuName = "Autumn Yard/Example1 Map Locator", order = 120)]
  public sealed class Example1MapLocator : BaseSceneLocator
  {
    [Header("Dependencies: Explicit dependencies")]

    [Header("Dictionaries")]
    [SerializeField] private AudioClipDictionaryAssetLocator soundLocator;

    [Header("Data-dependant")]
    [SerializeField] private PrefabArrayAssetLocator structureLocator;

    protected override IEnumerator LoadDependenciesOnly()
    {
      yield return CheckDependency(soundLocator);
      yield return CheckDependency<GameObject, Constants.Structure>(structureLocator);
    }

  }
}
