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

    public void Start()
    {
        _timer.AddDetaTimeEvent += Set;
        _timer.StopEvent += ResetTimer;
    }

    public void OnDestroy()
    {
        DisConnect();
        _timer.StopEvent -= ResetTimer;
    }

    public void DisConnect()
    {
        _timer.AddDetaTimeEvent -= Set;
    }

    public void Set()
    {
        _timeSliider.value = 1 - (_timer.GetTotalSecond_DeltaTime() / _goalTime);

        if (_timeSliider.value == 0)
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
