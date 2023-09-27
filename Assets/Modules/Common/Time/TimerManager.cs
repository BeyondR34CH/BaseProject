using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoSingleton<TimerManager>
{
    private int count = 0;
    [ShowInInspector, ReadOnly]
    private float timer = 0;

    public Action OneSecond;
    public Action HalfSecond;
    public Action QuarterSecond;

    private void Update()
    {
        if (Time.time >= timer)
        {
            timer += 0.25f;
            switch (count)
            {
                case 0:
                    OneSecond?.Invoke();
                    HalfSecond?.Invoke();
                    QuarterSecond?.Invoke();
                    count = 1;
                    break;
                case 1:
                    QuarterSecond?.Invoke();
                    count = 2;
                    break;
                case 2:
                    HalfSecond?.Invoke();
                    QuarterSecond?.Invoke();
                    count = 3;
                    break;
                case 3:
                    QuarterSecond?.Invoke();
                    count = 0;
                    break;
            }
        }
    }
}
