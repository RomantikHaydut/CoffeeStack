using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public event Action<int> OnAddingScore;

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnAddingScore(score);
    }

    public int GetScore()
    {
        return score;
    }
}
