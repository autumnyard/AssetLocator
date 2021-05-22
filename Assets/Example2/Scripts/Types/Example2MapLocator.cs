using System.Collections;
using UnityEngine;

namespace AutumnYard.Example2
{
  [CreateAssetMenu(fileName = "Example2 Map Locator", menuName = "Autumn Yard/Example2 Map Locator", order = 140)]
  public sealed class Example2MapLocator : BaseSceneLocator
  {
    // [Header("Dependencies: Enum dependant")]
    // // [SerializeField] private PrefabArrayAssetLocator structureLocator;
    // // [SerializeField] private AudioClipDictionaryAssetLocator soundLocator;
    // [SerializeField] private SpriteArrayAssetLocator effectsLocator;


    // public override void CheckDependenciesAndTriggerRemainFlags()
    // {
    //   // TriggerRemainFlag(soundLocator);
    //   // TriggerRemainFlag(structureLocator);
    //   TriggerRemainFlag(effectsLocator);
    // }

    // protected override IEnumerator LoadDependenciesOnly()
    // {
    //   // yield return CheckDependency(soundLocator);
    //   // yield return CheckDependency<GameObject>(structureLocator);
    //   yield return LoadDependency(effectsLocator);
    // }
  }
}
