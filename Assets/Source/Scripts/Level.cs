using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private float time=60f;

    private void Start()
    {
        Timer.Instance.SetTime(time);
        GameManager.Instance.level = gameObject;
        PointsGenerator.Instance.level = transform;
    }
}
