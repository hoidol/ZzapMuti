using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftTimeSlider : MonoBehaviour
{
    [SerializeField] private Slider _timeSliider;
    [SerializeField] private Timer _timer;

    [SerializeField] private float _goalTime;

    public event System.Action GoalEvent;

    private bool isConnected = false;

    public void Start()
    {
        Connect();
    }

    public void OnDestroy()
    {
        DisConnect();
        _timer.StopEvent -= ResetTimer;
    }

    public void Connect()
    {
        isConnected = true;
        _timer.AddDetaTimeEvent += Set;
        _timer.StopEvent += ResetTimer;
    }

    public void DisConnect()
    {

        isConnected = false;
        _timer.AddDetaTimeEvent -= Set;
    }

    public void SetGoalTime(float _goalT)
    {
        _goalTime = _goalT;
    }

    public void Set()
    {
        _timeSliider.value = 1 - (_timer.GetTotalSecond_DeltaTime() / _goalTime);

        if (_timeSliider.value <= 0)
        {
            GoalEvent?.Invoke();
            DisConnect();
        }
    }

    public void ResetTimer()
    {
        _timeSliider.value = 1;
    }
}
