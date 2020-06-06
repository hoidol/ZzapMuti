using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    [SerializeField] private Text text;

    [SerializeField] private Timer timer;

    private void Awake()
    {
        SetText();
    }

    public void Start()
    {
        timer.AddSecondEvent += SetText;
    }

    public void OnDestroy()
    {
        timer.AddSecondEvent -= SetText;
    }

    public void SetText()
    {
        text.text = $"{timer.Minute}:{timer.Second}";
    }
}
