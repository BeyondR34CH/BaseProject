using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref : NormalSingleton<Ref>
{
    public readonly static int AimLayer = 1 << LayerMask.NameToLayer("AimRaycast");

    public const string conditional = "UNITY_EDITOR";

    public static string FilePath(PathType type, string defaultPath = null)
    {
        return type switch
        {
            PathType.PersistentDataPath => Application.persistentDataPath,
            PathType.DataPath => Application.dataPath,
            PathType.StreamingAssetsPath => Application.streamingAssetsPath,
            PathType.CustomPath => defaultPath,
            _ => null,
        };
    }
}
