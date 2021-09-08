using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject level;
    private void Awake()
    {
        Instance = this;
    }

    public void StopGame()
    {
        level.SetActive(false);
        UIManager.Instance.ShowWinScreen();
    }
}
