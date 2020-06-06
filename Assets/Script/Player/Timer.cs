using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private float deltaTime;

    private int second;
    public int Second
    {
        get { return second; }
    }
    private int minute;
    public int Minute
    {
        get { return minute; }
    }

    private bool isPlay = false;

    public event System.Action AddSecondEvent;
    public event System.Action AddMinuteEvent;

    public void Awake()
    {
        TimerReset();
    }

    public void TimerReset()
    {
        deltaTime = 0;
        second = 0;
        minute = 0;
    }

    public void Play()
    {
        isPlay = true;
    }

    public void Pause()
    {
        isPlay = false;
    }

    public void Stop()
    {
        TimerReset();

        isPlay = false;
    }

    public void Update()
    {
        if (isPlay)
            AddTime();
    }

    public void AddTime()
    {
        deltaTime += Time.deltaTime;
        
        if (deltaTime >= 1)
        {
            second += 1;
            deltaTime -= 1;

            AddSecondEvent?.Invoke();
        }

        if (second >= 60)
        {
            second -= 60;
            minute += 1;
            AddMinuteEvent?.Invoke();
        }
    }

    public int GetTotalSecond()
    {
        return minute * 60 + second;
    }
}
