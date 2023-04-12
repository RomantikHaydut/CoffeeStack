using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action OnFinishLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void FinishLevel()
    {
        OnFinishLevel?.Invoke();
    }
}
