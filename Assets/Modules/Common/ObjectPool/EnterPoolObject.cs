using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoolObject : MonoBehaviour
{
    private Action<EnterPoolObject> EnterPool;

    public virtual void SetUp(Action<EnterPoolObject> EnterPool)
    {
        this.EnterPool = EnterPool;
    }

    protected virtual void OnDisable()
    {
        if (EnterPool == null)
        {
            Log.Error($"{name} can not enter pool, because EnterPool == null");
        }
        else
        {
            EnterPool.Invoke(this);
        }
    }
}
