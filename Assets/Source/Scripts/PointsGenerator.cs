using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGenerator : MonoBehaviour
{
    public static PointsGenerator Instance { get; private set; }

    [SerializeField]
    private int _columnCount = 6;
    [SerializeField]
    private GameObject _columnObj;
    [SerializeField]
    private GameObject[] _points;
    [SerializeField]
    private float _leftPointPositionX = -2f;
    [SerializeField]
    private float _startInstantPointPositionY = 5f;
    [SerializeField]
    private float _pointsOffsetX = 0.8f;

    public Transform level;

    [HideInInspector]
    public List<Column> columns;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for(int i = 0; i < _columnCount; i++)
        {
            columns.Add(Instantiate(_columnObj, transform).GetComponent<Column>());
            columns[i].id = i;
        }
    }
    public void ResetPointToGenerate()
    {
        for (int i = 0; i < _columnCount; i++)
        {
            columns[i].ResetCount();
        }
    }
    public void GeneratePoints()
    {
        for(int i = 0; i < columns.Count; i++)
        {

            if (columns[i].GetCount() != 0)
            {
                for (int j=0;j< columns[i].GetCount(); j++)
                {
                    Vector3 pos = Vector3.zero;
                    if (i == 0)
                    {
                        pos = new Vector3(_leftPointPositionX, _startInstantPointPositionY + (float)j, 0f);
                    }
                    else
                    {
                        pos = new Vector3(_leftPointPositionX + (_pointsOffsetX * (float)i), _startInstantPointPositionY + (float)j, 0f);
                        
                    }
                    int rand = Random.Range(0, _points.Length);
                    Point tmp = Instantiate(_points[rand], pos, transform.rotation).GetComponent<Point>();
                    tmp.transform.parent = level;
                    tmp.columnId = i;
                }
            }
        }
        ResetPointToGenerate();
    }
    public void AddPointToGenerate(int id)
    {
        columns[id].IncrementCount();
    }
}
