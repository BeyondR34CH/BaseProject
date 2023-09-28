using Cinemachine;
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
    private static CinemachineBrain camBrain;

    public static Camera MainCamera
    {
        get
        {
            if (!mainCamera) MainCamera = Camera.main;
            return mainCamera;
        }
        private set
        {
            if (mainCamera != value)
            {
                mainCamera = value;
                Log.Info($"Current MainCamera: {mainCamera.name}");
                CamBrain = mainCamera.GetComponent<CinemachineBrain>();
                OnMainCameraChange?.Invoke();
            }
        }
    }

    public static CinemachineBrain CamBrain
    {
        get
        {
            if (!mainCamera) MainCamera = Camera.main;
            return camBrain;
        }
        private set
        {
            if (camBrain != value)
            {
                camBrain = value;
                if (camBrain) Log.Info($"Current CamBrain: {camBrain.name}");
            }
        }
    }

    public static ICinemachineCamera LiveCamera => CamBrain.ActiveVirtualCamera;

    public static Transform Target
    {
        set
        {
            if (LiveCamera == null) return;
            LiveCamera.Follow = value;
            LiveCamera.LookAt = value;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void ListenMainCameraChange()
    {
        SceneManager.activeSceneChanged += (from, to) => MainCamera = Camera.main;
    }
}
