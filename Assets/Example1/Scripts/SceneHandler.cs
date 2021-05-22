using System.Collections;
using UnityEngine;

namespace AutumnYard.Example1
{
  public class SceneHandler : MonoBehaviour
  {
    private Constants.Map currentMap = Constants.Map.None;

    [SerializeField] private Example1MapLocator[] maps;
    [SerializeField] private Logger.Type enableTypes;


    private void Awake()
    {
      Logger.enabledTypes = enableTypes;
    }

#if UNITY_EDITOR
    [ContextMenu("Change to map 1")] private void LoadMap1() => StartCoroutine(ChangeMap(Constants.Map.Map1));
    [ContextMenu("Change to map 2")] private void LoadMap2() => StartCoroutine(ChangeMap(Constants.Map.Map2));
    [ContextMenu("Change to map 3")] private void LoadMap3() => StartCoroutine(ChangeMap(Constants.Map.Map3));
#endif

    private IEnumerator LoadMap(Constants.Map to)
    {
      currentMap = to;
      yield return maps[(int)currentMap].Load();
      yield return maps[(int)currentMap].Activate();
    }

    private IEnumerator UnloadMap(Constants.Map which)
    {
      if (which == Constants.Map.None) yield break;
      if (!maps[(int)which].IsLoaded) yield break;

      yield return maps[(int)which].Unload();
    }

    private IEnumerator ChangeMap(Constants.Map newMap)
    {
      if (currentMap == newMap) yield break;

      yield return UnloadMap(currentMap);
      yield return LoadMap(newMap);
    }

  }
}
