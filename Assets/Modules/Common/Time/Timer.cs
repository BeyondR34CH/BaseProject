using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    public float targetTime;
    public bool autoSub;

    private float timer = 0;

    public Timer(float targetTime, bool autoSub = true)
    {
        this.targetTime = targetTime;
        this.autoSub = autoSub;
    }

    public bool OnReach(bool reset = true)
    {
        if (timer >= targetTime)
        {
            if (reset) Reset();
            return true;
        }
        else return false;
    }

    public bool Update(float multiplier = 1)
    {
        return UpdateTimer(Time.deltaTime, multiplier);
    }

    public bool FixedUpdate(float multiplier = 1)
    {
        return UpdateTimer(Time.fixedDeltaTime, multiplier);
    }

    public bool UpdateTimer(float delta, float multiplier)
    {
        timer += delta * multiplier;
        if (timer >= targetTime)
        {
            if (autoSub) timer -= targetTime;
            return true;
        }
        else return false;
    }

    public void Reset()
    {
        timer = 0;
    }
}
