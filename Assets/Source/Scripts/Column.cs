using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public int id = 0;
    private int count = 0;

    public int GetCount()
    {
        return count;
    }
    public void IncrementCount()
    {
        count++;
    }
    public void ResetCount()
    {
        count = 0;
    }
}
