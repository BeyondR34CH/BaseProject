using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Linq;
using System;

public abstract class Saver<Data> : MonoSingleton<Saver<Data>> where Data : class
{
    [SerializeField]
    protected PathType savePathType;
    [SerializeField, ShowIf("savePathType", PathType.CustomPath)]
    protected string customSavePath;
    [SerializeField]
    protected SaveNameType dataNameType;
    [SerializeField, ShowIf("dataNameType", SaveNameType.CustomName)]
    protected string customDataName;
    [SerializeField]
    private string suffix = ".save";
    [SerializeField, ReadOnly]
    private string curFileName = "Null";

    [SerializeReference] protected Data curData;
    [SerializeReference] protected List<string> fileList;

    public Action OnSaveCompleted;
    public Action OnLoadCompleted;

    public Data CurData => curData;
    public List<string> FileList => fileList;
    public string FilePath => Ref.FilePath(savePathType, customSavePath);
    public string DataName => dataNameType switch
    {
        SaveNameType.ProductName => Settings.Ins.build.name,
        SaveNameType.CustomName => customDataName,
        _ => null,
    };

    protected override void Awake()
    {
        base.Awake();

        if (string.IsNullOrEmpty(DataName) || string.IsNullOrEmpty(FilePath))
        {
            Log.Error("The name and path of save file can not be empty");
        }
        else curFileName = DataName + 0 + suffix;
    }

    [ResponsiveButtonGroup]
    public Data Save()
    {
        string path = FilePath + '/' + curFileName;
        if (File.Exists(path))
        {
            using FileStream stream = File.Open(path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, curData);
            Log.Info($"Save successfully: {path}");
            OnSaveCompleted?.Invoke();
            return curData;
        }
        else
        {
            Log.Error($"Could not find {path}");
            return null;
        }
    }

    protected abstract Data GetNewData();

    [ResponsiveButtonGroup]
    public Data Create(bool setCurData = true)
    {
        if (curFileName == "Null")
        {
            Log.Error("The name and path of save file can not be empty");
            return null;
        }
        Data data = GetNewData();
        string fileName = curFileName, path = FilePath + '/' + fileName;
        for (int num = 0; File.Exists(path); num++)
        {
            fileName = DataName + num + suffix;
            path = FilePath + '/' + fileName;
        }
        using FileStream stream = File.Open(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        fileList.Add(fileName);
        Log.Info($"Save successfully: {path}");
        if (setCurData)
        {
            curData = data;
            curFileName = fileName;
        }
        OnSaveCompleted?.Invoke();
        return data;
    }

    [Button(Expanded = true)]
    public Data Load(string fileName, bool setCurData = true)
    {
        string path = FilePath + '/' + fileName;
        if (File.Exists(path))
        {
            using FileStream stream = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;
            Log.Info($"Load successfully: {path}");
            if (setCurData)
            {
                curData = data;
                curFileName = fileName;
                if (!fileList.Contains(fileName))
                {
                    Log.Warning($"{fileName} does not exist in fileList, adding...");
                    fileList.Add(fileName);
                }
                OnLoadCompleted?.Invoke();
            }
            return data;
        }
        else
        {
            Log.Error($"Could not load {path}");
            return null;
        }
    }

    [ResponsiveButtonGroup]
    public Data Reload()
    {
        curData = Load(curFileName);
        return curData;
    }

    [ResponsiveButtonGroup]
    public List<string> GetSaveFiles()
    {
        fileList = Directory.GetFiles(FilePath, '*' + suffix).ToList();
        for (int i = 0; i < fileList.Count; i++)
        {
            fileList[i] = fileList[i].Replace(FilePath, "")[1..];
        }
        return fileList;
    }

    [ResponsiveButtonGroup]
    public void ClearCurData()
    {
        curData = GetNewData();
        Save();
    }
}
