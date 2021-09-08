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
    private GameObject _point;

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
                        pos = new Vector3(-2f, 5f + (float)j, 0f);
                    }
                    else
                    {
                        pos = new Vector3(-2f +(0.8f*(float)i), 5f + (float)j,0f);
                        
                    }

                    Point tmp=Instantiate(_point, pos, transform.rotation).GetComponent<Point>();
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
