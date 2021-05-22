using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.Example1
{
  public class SceneHandler : MonoBehaviour
  {
    private Constants.Map currentMap = Constants.Map.None;

    [SerializeField] private BaseAssetManager assetManager;
    [SerializeField] private Example1MapLocator[] maps;
    [SerializeField] private Logger.Type enableLoggingTypes;


    private void Awake()
    {
      Logger.enabledTypes = enableLoggingTypes;
    }

    private void Update()
    {
      if (Input.GetKeyUp(KeyCode.Alpha1)) LoadMap1();
      else if (Input.GetKeyUp(KeyCode.Alpha2)) LoadMap2();
      else if (Input.GetKeyUp(KeyCode.Alpha3)) LoadMap3();
    }

    private void LoadMap1() => StartCoroutine(ChangeMap(Constants.Map.Map1));

    private void LoadMap2() => StartCoroutine(ChangeMap(Constants.Map.Map2));

    private void LoadMap3() => StartCoroutine(ChangeMap(Constants.Map.Map3));


    private IEnumerator ChangeMap(Constants.Map newMap)
    {
      if (currentMap == newMap) yield break;

      // Trigger the FlagRemain for the assets that should persist
      maps[(int)newMap].CheckDependenciesAndTriggerRemainFlags();

      yield return UnloadMap(currentMap);
      yield return assetManager.CheckFlagRemains();
      yield return LoadMap(newMap);
    }

    // private IEnumerator UnloadUnnecessaryDependencies()
    // {
    //   // Unload asset dependencies without the FlagRemain
    //   assetManager.GetLoadersToUnload(out List<Loader> toUnload);
    //   for (int i = 0; i < toUnload.Count; i++)
    //   {
    //     yield return toUnload[i].Unload();
    //   }
    // }

    private IEnumerator UnloadMap(Constants.Map which)
    {
      if (which == Constants.Map.None) yield break;
      if (!maps[(int)which].IsLoaded) yield break;

      yield return maps[(int)which].Unload();

    }

    private IEnumerator LoadMap(Constants.Map to)
    {
      currentMap = to;
      yield return maps[(int)currentMap].Load();
      yield return maps[(int)currentMap].Activate();
    }


  }
}
