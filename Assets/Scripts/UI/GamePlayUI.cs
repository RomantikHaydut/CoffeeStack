using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject settingUI;

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

    public void openSettings()
    {
        settingUI.SetActive(true);
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
