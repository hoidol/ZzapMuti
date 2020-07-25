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

    private int goalSecond = -1;
    public int GoalSecond
    {
        get { return goalSecond; }
        set { goalSecond = value; }
    }

    private bool isPlay = false;

    public event System.Action AddDetaTimeEvent;
    public event System.Action AddSecondEvent;
    public event System.Action AddMinuteEvent;

    public event System.Action GoalSecondEvent;

    public event System.Action PlayEvent;
    public event System.Action PauseEvent;
    public event System.Action StopEvent;

    public void Awake()
    {
        TimerReset();
    }

    public void TimerReset()
    {
        deltaTime = 0;
        second = 0;
        minute = 0;

        goalSecond = -1;
        GoalSecondEvent = null;
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

        StopEvent?.Invoke();

        isPlay = false;
    }

    public void Update()
    {
        if (isPlay)
            AddTime();

        if (goalSecond != -1)
            if (IsGoal())
                GoalSecondEvent?.Invoke();
    }

    public bool IsGoal()
    {
        if (GetTotalSecond() >= goalSecond)
            return true;
        else return false;
    }

    public void AddTime()
    {
        deltaTime += Time.deltaTime;
        AddDetaTimeEvent?.Invoke();

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

    public float GetTotalSecond_DeltaTime()
    {
        return minute * 60 + second+deltaTime;
    }
}
