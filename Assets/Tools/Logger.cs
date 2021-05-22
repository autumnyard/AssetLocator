using UnityEngine;

namespace AutumnYard
{
  public static class Logger
  {
    [System.Flags]
    public enum Type
    {
      DictAssetLocator = 1 << 1,
      ArrayAssetLocator = 1 << 2,
      MapLocator = 1 << 3,
      AssetManager = 1 << 4,
      Example1 = 1 << 5,
      Example2 = 1 << 6,
    }

    static public Type enabledTypes;

#if LOGGING
#if UNITY_EDITOR
    static private (string color, string prefix, Type type) Process(Object obj)
    {
      switch (obj)
      {
        // blue brown cyan darkblue green lime magenta maroon olive orange purple red teal yellow

        // Directors
        //   case BaseMapLocator s: return ("orange", "Map Locator", Type.MapLocator);

        default: return ("white", "-", Type.DictAssetLocator);
      }
    }
#endif
#endif

    public static void Log(this Object obj, string message)
    {
      Log(message, obj);
    }

    public static void Log(string message, Object obj = null)
    {
#if LOGGING
#if UNITY_EDITOR
      var (color, prefix, type) = Process(obj);

      if (!enabledTypes.HasFlag(type)) return;

      Debug.Log($"<color={color}>[{prefix}]: {message}</color>", obj);
#else
      Debug.Log($"{message}");
#endif
#endif
    }

    public static void Log(string message, Type type)
    {
#if LOGGING
      if (!enabledTypes.HasFlag(type)) return;

#if UNITY_EDITOR
      Debug.Log($"<color=white>[{type}]: {message}</color>");
#else
      Debug.Log($"{message}");
#endif
#endif
    }

    public static void LogError(this Object obj, string message)
    {
      LogError(message, obj);
    }

    public static void LogError(string message, Object obj = null)
    {
#if LOGGING
#if UNITY_EDITOR
      var (color, prefix, type) = Process(obj);

      if (!enabledTypes.HasFlag(type)) return;

      Debug.LogError($"<color={color}>[{prefix}]:<color=red> {message}</color></color>", obj);
#else
      Debug.LogError($"{message}");
#endif
#endif
    }

    public static void LogError(string message, Type type)
    {
#if LOGGING
      if (!enabledTypes.HasFlag(type)) return;

#if UNITY_EDITOR
      Debug.LogError($"<color=white>[{type}]:<color=red> {message}</color></color>");
#else
      Debug.LogError($"{message}");
#endif
#endif
    }

    public static void EmptyLine(Type type)
    {
#if LOGGING
      if (!enabledTypes.HasFlag(type)) return;

      Debug.Log("");
#endif
    }

  }
}
