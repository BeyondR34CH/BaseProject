using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NormalSingleton<GameManager>
{
    public int targetFrameRate = 60;

    public GameManager()
    {

    }

    [RuntimeInitializeOnLoadMethod]
    public static void InitGame()
    {
        Application.targetFrameRate = Ins.targetFrameRate;
    }
}
