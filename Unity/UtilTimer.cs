using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilTimer : MonoBehaviour
{
    public Action timerCallback;
    public bool loop;

    float timerStart;
    float timer;

    public void SetTimer(float timer)
    {
        timerStart = timer;
        this.timer = timer;
    }

    public void SetTimer(float timer, Action timerCallback)
    {
        timerStart = timer;
        this.timer = timer;
        this.timerCallback = timerCallback;
    }

    public void AddAction(Action timerCallback)
    {
        this.timerCallback += timerCallback;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (IsComplete())
        {
            timerCallback();

            if (!loop) return;

            timer = timerStart;
        }
    }

    public void Clear()
    {
        timerCallback = null;
        timer = 0;
    }

    public bool IsComplete()
    {
        return timer <= 0;
    }

    public float TimeElapsed()
    {
        return timerStart - timer;
    }

    public float TimeRemaining()
    {
        return timer;
    }
}
