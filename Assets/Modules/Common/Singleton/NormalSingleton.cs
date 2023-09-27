using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalSingleton<T> : ISingleton<T> where T : NormalSingleton<T>, new()
{
    private static T ins;

    public static T Ins => ins ??= new();

    protected NormalSingleton()
    {
        Log.Info($"NormalSingleton {typeof(T)} Start Init");
    }
}
