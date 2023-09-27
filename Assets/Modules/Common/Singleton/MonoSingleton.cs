using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton<T> where T : MonoSingleton<T>
{
    public static T Ins { get; private set; }

    protected virtual void Awake()
    {
        InitSingleton(this as T);
    }

    public static T InitSingleton(T t)
    {
        if (!Ins)
        {
            Log.Info($"MonoSingletion {typeof(T)}(name: {t.name}) Start Init");
            Ins = t;
        }
        else
        {
            Log.Error($"There are two MonoSingletion: {Ins.name}, {t.name}");
            GameObject.Destroy(t);
        }
        return Ins;
    }
}
