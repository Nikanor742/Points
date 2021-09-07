using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Vector3 _lineStartPos;
    private bool _activate = false;

    public void PointerDown()
    {
        _lineStartPos = Camera.main.ScreenToWorldPoint(transform.position);
        TouchController.Instance.StartSwipe(_lineStartPos);
        _activate = true;
    }
    public void PointerEnter()
    {
        Debug.Log("enter");
        if (!_activate)
        {
            _activate = true;
            _lineStartPos = Camera.main.ScreenToWorldPoint(transform.position);
            _lineStartPos.z = 0;
            TouchController.Instance.DrawLine(_lineStartPos, Color.red);
        }
    }
}
