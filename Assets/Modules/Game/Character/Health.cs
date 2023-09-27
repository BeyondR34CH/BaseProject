using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool invincible;
    public TargetStat<float> health;

    public Action<float> Hurt;
    public Action Die;

    private void OnEnable()
    {
        health.ToTarget();
    }

    public void TakeDamage(float value)
    {
        if (invincible) return;

        health.value -= value;
        Hurt?.Invoke(value);

        if (health.value <= 0)
        {
            health.value = 0;
            Die?.Invoke();
        }
    }

    public void Heal(float value)
    {
        if (health.value == health.target) return;

        health.value += value;
        if (health.value > health.target)
        {
            health.ToTarget();
        }
    }
}
