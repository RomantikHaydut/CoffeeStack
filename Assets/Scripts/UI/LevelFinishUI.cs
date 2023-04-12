using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishUI : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        nextLevelButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
        });

        GameManager.Instance.OnFinishLevel += Show;

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
