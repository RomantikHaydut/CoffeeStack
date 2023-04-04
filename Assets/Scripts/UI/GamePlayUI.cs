using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        ScoreManager.Instance.OnAddingScore += ScoreManager_OnAddingScore;
    }

    private void ScoreManager_OnAddingScore(int obj)
    {
        DisplayScore(obj);
    }

    private void DisplayScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
