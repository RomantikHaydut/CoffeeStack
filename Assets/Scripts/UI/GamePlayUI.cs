using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsUI;

    private void Start()
    {
        ScoreManager.Instance.OnAddingScore += ScoreManager_OnAddingScore;
        settingsButton.onClick.AddListener(() =>
        {
            settingsUI.SetActive(true);
            Time.timeScale = 0;
        });
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
