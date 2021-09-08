using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _activate = false;
    public int columnId = 0;
    public int colorId = 0;

    private void OnMouseDown()
    {
        _activate=PointsController.Instance.StartSwipe(transform.position,this,colorId);
    }

    private void OnMouseEnter()
    {
        if (!_activate)
        {
            _activate = PointsController.Instance.AddPoint(this,transform.position,colorId);
        }
    }
}
