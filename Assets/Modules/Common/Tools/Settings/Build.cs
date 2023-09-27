using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Build
{
    public string name;

    [SerializeField] private int primary;
    [SerializeField] private int secondary;
    [SerializeField] private int number;

    public string Version => $"{primary}.{secondary}.{number}";

    public string NextVersion()
    {
        secondary++;
        number = 0;
        return Version;
    }

    public string FormalVersion()
    {
        primary++;
        secondary = 0;
        number = 0;
        return Version;
    }

    public string ResetVersion()
    {
        primary = 0;
        secondary = 0;
        number = 0;
        return Version;
    }
}
