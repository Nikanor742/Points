using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public static PointsController Instance { get; private set; }

    public bool swipe = false;
    public List<GameObject> lines;
    public Camera mainCamera;
    [SerializeField]
    private Material _lineMaterial;
    [SerializeField]
    private float _lineWidth = 0.2f;

    private Vector3 startPos;
    private int pointCounter = 0;
    private int currentColor = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main; 
    }

    public bool StartSwipe(Vector3 startPosition,Point point,int colorId)
    {
        if (!swipe)
        {
            swipe = true;
            currentColor = colorId;
            startPos = startPosition;
            lines.Add(point.gameObject);
            pointCounter++;
            PointsGenerator.Instance.AddPointToGenerate(point.columnId);
            return true;
        }
        return false;
    }

    private void DrawRealTime(Vector3 start, Vector3 end, Color color, float duration)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = _lineMaterial;
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = _lineWidth;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }

    public void DrawLine(Vector3 end, Color color)
    {
        if (swipe)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = startPos;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = _lineMaterial;
            lr.startColor = lr.endColor = color;
            lr.startWidth = lr.endWidth = _lineWidth;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, end);
            startPos = end;
            lines.Add(myLine);
        }
        
    }

    public bool AddPoint(Point pointObj,Vector3 pos,int colorId)
    {
        if (swipe && colorId == currentColor)
        {
            Debug.Log(Vector3.Distance(startPos, pos));
            if(Vector3.Distance(startPos, pos) <= 0.85f)
            {
                DrawLine(pos, Color.red);
                lines.Add(pointObj.gameObject);
                pointCounter++;
                PointsGenerator.Instance.AddPointToGenerate(pointObj.columnId);
                return true;
            }
        }
        return false;
    }

    private void DestroyPoints()
    {
        if (pointCounter!=1)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Destroy(lines[i]);
            }
            PointsGenerator.Instance.GeneratePoints();
        }
        else
        {
            PointsGenerator.Instance.ResetPointToGenerate();
        }
        lines.Clear();
        pointCounter = 0;
    }

    private void Update()
    {
        if (swipe)
        {
            Vector3 tmp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            tmp.z = 0;
            DrawRealTime(startPos, tmp, Color.red, 0.02f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
            currentColor = 0;
            DestroyPoints();
        }
        
    }
}
