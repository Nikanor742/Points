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
    private float _lineWidth = 0.2f;
    [SerializeField]
    private Material[] _materials;
    [SerializeField]
    private float _activateDistance = 0.85f;
    [SerializeField]
    private float _timeToDestroyLine = 0.02f;

    private Material _currentMaterial;
    private Vector3 startPos;
    private int pointCounter = 0;
    private int currentColor = 1;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        _currentMaterial = _materials[0];
    }

    public int GetScore()
    {
        return score;
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
            _currentMaterial = _materials[point.colorId];
            return true;
        }
        return false;
    }

    private void DrawRealTime(Vector3 start, Vector3 end, float duration)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = _currentMaterial;
        lr.startWidth = lr.endWidth = _lineWidth;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }

    public void DrawLine(Vector3 end)
    {
        if (swipe)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = startPos;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = _currentMaterial;
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
            if(Vector3.Distance(startPos, pos) <= _activateDistance)
            {
                DrawLine(pos);
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
            score += pointCounter;
            UIManager.Instance.SetScoreText(score);
        }
        else
        {
            PointsGenerator.Instance.ResetPointToGenerate();
            lines[0].GetComponent<Point>().activate = false;
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
            DrawRealTime(startPos, tmp, _timeToDestroyLine);
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
            currentColor = 0;
            DestroyPoints();
        }
        
    }
}
