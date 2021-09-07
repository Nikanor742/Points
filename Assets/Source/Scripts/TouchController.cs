using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    public static TouchController Instance { get; private set; }

    public Material lineMaterial;
    public Vector3 startPos;

    private bool _swipe = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSwipe(Vector3 startPosition)
    {
        if (!_swipe)
        {
            _swipe = true;
            startPos = startPosition;
        }
    }

    private void DrawRealTime(Vector3 start, Vector3 end, Color color, float duration)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.SetColors(color, color);
        lr.SetWidth(0.2f, 0.2f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    public void DrawLine(Vector3 end, Color color)
    {
        if (_swipe)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = startPos;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.SetColors(color, color);
            lr.SetWidth(0.2f, 0.2f);
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, end);
            Debug.Log(startPos+":"+ end);
            startPos = end;
        }
        
    }



    private void Update()
    {
        if (_swipe)
        {
            startPos.z = 0;
            Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tmp.z = 0;
            DrawRealTime(startPos, tmp, Color.red, 0.02f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _swipe = false;
        }
        
    }
}
