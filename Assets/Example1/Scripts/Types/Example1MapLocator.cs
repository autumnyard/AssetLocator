using System.Collections;
using UnityEngine;

namespace AutumnYard.Example1
{
  [CreateAssetMenu(fileName = "Example1 Map Locator", menuName = "Autumn Yard/Example1 Map Locator", order = 120)]
  public sealed class Example1MapLocator : BaseSceneLocator
  {
    [Header("Dependencies: Explicit dependencies")]
    [SerializeField] private AudioClipDictionaryAssetLocator soundLocator;
    [SerializeField] private PrefabArrayAssetLocator structureLocator;

    public override void CheckDependenciesAndTriggerRemainFlags()
    {
      TriggerRemainFlag(soundLocator);
      TriggerRemainFlag(structureLocator);
    }

    protected override IEnumerator LoadDependenciesOnly()
    {
      yield return LoadDependency(soundLocator);
      yield return LoadDependency(structureLocator);
    }

  }
}
