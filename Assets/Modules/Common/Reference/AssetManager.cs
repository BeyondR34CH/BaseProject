using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AssetManager : MonoSingleton<AssetManager>
{
    public static void Load<T>(string path, string name, Action<T> callback) where T : UnityEngine.Object
    {
        Ins.StartCoroutine(UnityWebRequestLoad(path, name, callback));
    }

    public static IEnumerator UnityWebRequestLoad<T>(string path, string name, Action<T> callback) where T : UnityEngine.Object
    {
        var uri = new Uri(Path.Combine(Application.dataPath, path));
        var request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            callback?.Invoke(bundle.LoadAsset<T>(name));
        }
        else
        {
            Log.Error(request.error);
        }
    }
}
