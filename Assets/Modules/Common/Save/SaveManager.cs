using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SaveManager : Saver<SaveData>
{
    protected override void Awake()
    {
        base.Awake();

        var list = GetSaveFiles();

        if (list.Count <= 0) 
            Create();
        else 
            Load(FileList[0]);
    }

    protected override SaveData GetNewData()
    {
        SaveData newData = new();
        return newData;
    }
}

[System.Serializable]
public class SaveData
{
    public string name;
    [SerializeField] private float time;
    [SerializeField] private int experience;
    [SerializeField] private int level;

    public float Time => time;
    public int Experience => experience;
    public int Level => level;
}