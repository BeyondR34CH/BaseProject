using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectSettings", menuName = "ScriptableObject/Settings")]
public class Settings : SOSingleton<Settings>
{
    public Build build;
    public RunningState runningState;

    public bool IsTest => runningState == RunningState.Test;
    public bool IsPerformanceTest => runningState == RunningState.PerformanceTest;
    public bool IsNormal => runningState == RunningState.Normal;

#if UNITY_EDITOR

    [SerializeField, Space(20)]
    private PathType buildABPathType = PathType.StreamingAssetsPath;
    [SerializeField, ShowIf("buildABPathType", PathType.CustomPath)]
    private string customBuildABPath;

    [MenuItem("Tools/Build All Asset Bundles")]
    public static void BuildAllAB()
    {
        string strABOutPAthDir = Ref.FilePath(Ins.buildABPathType, Ins.customBuildABPath);

        if (!Directory.Exists(strABOutPAthDir)) Directory.CreateDirectory(strABOutPAthDir);
        BuildPipeline.BuildAssetBundles(strABOutPAthDir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    public static void Select(string path)
    {
        var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
        EditorGUIUtility.PingObject(obj);
        Selection.activeObject = obj;
    }

    [MenuItem("Selector/Project Settings")]
    public static void SelectSettings() => Select("Assets/Resources/ProjectSettings.asset");

    [MenuItem("Selector/Persistence")]
    public static void SelectPersistence() => Select("Assets/Resources/Persistence.prefab");

    [MenuItem("Selector/Luban Export Config")]
    public static void SelectLubanExportConfig() => Select("Assets/Resources/LubanExportConfig.asset");

    [Button("Update Settings")]
    public static void UpdateSettings()
    {
        PlayerSettings.productName = Ins.build.name;
        PlayerSettings.bundleVersion = Ins.build.Version;

        Log.Info($"Update Settings, Current Version: {Ins.build.Version}");
    }

#endif

}