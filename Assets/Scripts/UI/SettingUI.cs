using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrationButton;
    [SerializeField] private Button closeButton;

    private bool isSoundOn = true;
    private bool isVibrationOn = true;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        });

        soundButton.onClick.AddListener(() =>
        {
            ToggleSound();
            ChangeButtonColorAndText(soundButton, isSoundOn);
        });

        vibrationButton.onClick.AddListener(() =>
        {
            ToggleSound();
            ChangeButtonColorAndText(vibrationButton, isSoundOn);
        });

        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            // Sound Volume = 1
        }
        else
        {
            // sound volume = 0;
        }
        print("isVibrate : " + isSoundOn);
    }

    private void ToggleVibration()
    {
        isVibrationOn = !isVibrationOn;
        if (isVibrationOn)
        {
            // Sound Volume = 1
        }
        else
        {
            // sound volume = 0;
        }
        print("isVibrate : " + isVibrationOn);
    }

    private void ChangeButtonColorAndText(Button button , bool isOn)
    {
        TMP_Text text = button.GetComponentInChildren<TMP_Text>();
        if (isOn)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.green;
            colorBlock.selectedColor = Color.green;
            button.colors=colorBlock;
            text.text = "On";
        }
        else
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.red;
            colorBlock.selectedColor = Color.red;
            button.colors = colorBlock;
            text.text = "Off";
        }

    }

}
