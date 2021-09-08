using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _winScreenScore;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    public GameObject winScreen;

    private void Awake()
    {
        Instance = this;
    }

    public void SetScoreText(int value)
    {
        _scoreText.text ="Счет:  " + value.ToString();
    }

    public void SetTime(float value)
    {
        _timeText.text = "Время:" + Mathf.Round(value).ToString();
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        int score = PointsController.Instance.GetScore();
        _winScreenScore.text = score.ToString();
        _bestScoreText.text = PlayerPrefs.GetInt("BestScore", score).ToString();
        SaveScore(score);
        
    }

    public void SaveScore(int score)
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
}
