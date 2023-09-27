using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    [System.Diagnostics.Conditional(Ref.conditional)]
    public static void Info(object message)
    {
        Debug.Log(message);
    }

    [System.Diagnostics.Conditional(Ref.conditional)]
    public static void Warning(object message)
    {
        Debug.LogWarning(message);
    }

    [System.Diagnostics.Conditional(Ref.conditional)]
    public static void Error(object message)
    {
        Debug.LogError(message);
    }
}
