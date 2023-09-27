using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOSingleton<T> : ScriptableObject, ISingleton<T> where T : SOSingleton<T>
{
    private static T ins;

    public static T Ins
    {
        get
        {
            if (!ins)
            {
                string type = typeof(T).Name;
                ins = Resources.Load<T>($"Project{type}");
                if (!ins) Log.Error($"{type} not Found under SOSingletons/{type}");
            }
            return ins;
        }
    }
}
