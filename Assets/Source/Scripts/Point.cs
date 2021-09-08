using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool activate = false;
    public int columnId = 0;
    public int colorId = 0;
    [SerializeField]
    private Animator _anim;

    private void OnMouseDown()
    {
        activate=PointsController.Instance.StartSwipe(transform.position,this,colorId);
        _anim.SetTrigger("Enter");
    }

    private void OnMouseEnter()
    {
        if (!activate)
        {
            activate = PointsController.Instance.AddPoint(this,transform.position,colorId);
            if (activate)
            {
                _anim.SetTrigger("Enter");
            }
        }
    }
}
