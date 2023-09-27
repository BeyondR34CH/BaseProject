using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : NormalSingleton<CameraManager>
{
    public static event Action OnMainCameraChange;

    private static Camera mainCamera;

    public static Camera MainCamera
    {
        get
        {
            if (!mainCamera) MainCamera = Camera.main;
            return mainCamera;
        }
        set
        {
            if (mainCamera != value)
            {
                mainCamera = value;
                Log.Info($"Current MainCamera(name: {mainCamera.name})");
                OnMainCameraChange?.Invoke();
            }
            mainCamera = value;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void ListenMainCameraChange()
    {
        SceneManager.activeSceneChanged += (from, to) => MainCamera = Camera.main;
    }
}
