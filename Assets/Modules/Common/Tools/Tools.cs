using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    [System.Diagnostics.Conditional(Ref.conditional)]
    public static void SetName(this GameObject go, string name)
    {
        go.name = name;
    }

    public static Vector3 To3(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
}
