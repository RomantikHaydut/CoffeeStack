using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;
    [SerializeField] private Button vibrationOnButton;
    [SerializeField] private Button vibrationOffButton;
    [SerializeField] private Button closeButton;

    private bool canVibrate = true;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        });

        soundOnButton.onClick.AddListener(() =>
        {
            soundOffButton.gameObject.SetActive(true);
            soundOnButton.gameObject.SetActive(false);
            SoundManager.Instance.SoundOff();
        }); 

        soundOffButton.onClick.AddListener(() =>
        {
            soundOffButton.gameObject.SetActive(false);
            soundOnButton.gameObject.SetActive(true);
            SoundManager.Instance.SoundOn();
        });

        vibrationOnButton.onClick.AddListener(() =>
        {
            vibrationOffButton.gameObject.SetActive(true);
            vibrationOnButton.gameObject.SetActive(false);
            canVibrate = false;
        });

        vibrationOffButton.onClick.AddListener(() =>
        {
            vibrationOffButton.gameObject.SetActive(false);
            vibrationOnButton.gameObject.SetActive(true);
            canVibrate = true;
        });

        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    public void Vibrate()
    {
        if (canVibrate)
        {
            Handheld.Vibrate();
        }
    }

}
