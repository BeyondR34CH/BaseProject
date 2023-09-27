using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, InlineProperty]
public struct TargetStat<T>
{
    [HorizontalGroup, HideLabel, ReadOnly]
    public T value;
    [HorizontalGroup(0.5f), HideLabel]
    public T target;

    public static implicit operator T(TargetStat<T> stat)
    {
        return stat.value;
    }

    public void ToTarget()
    {
        value = target;
    }
}
