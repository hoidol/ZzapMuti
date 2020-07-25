using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOverDamage : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private LeftTimeSlider _timerOverSlider;

    private bool isTimeOver = false;

    public void OnEnable()
    {
        _timerOverSlider.GoalEvent += TimeOver;
        _timer.StopEvent += ResetTime;
    }

    public void OnDisable()
    {
        _timerOverSlider.GoalEvent -= TimeOver;
        _timer.StopEvent -= ResetTime;
    }

    public void TimeOver()
    {
        isTimeOver = true;

        _timer.AddSecondEvent += Damage;
    }

    public void ResetTime()
    {
        isTimeOver = false;

        _timer.AddSecondEvent -= Damage;
    }

    public void Damage()
    {
        //if (!isTimeOver)
          //  return;

        UnitManager.Instance.TimeOver();
    }
}
