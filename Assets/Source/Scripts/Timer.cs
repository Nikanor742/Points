using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    private float _time;
    private bool _activate = true;

    private void Awake()
    {
        Instance = this;
    }
    public void SetTime(float value)
    {
        _time = value;
    }

    void Update()
    {
        if (_activate)
        {
            _time -= Time.deltaTime;
            UIManager.Instance.SetTime(_time);
            if (_time <= 0)
            {
                _activate = false;
                GameManager.Instance.StopGame();
            }
        }
        
    }
}
