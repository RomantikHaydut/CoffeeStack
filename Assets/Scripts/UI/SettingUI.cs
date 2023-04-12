using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject soundOff_button;
    [SerializeField] private GameObject soundOnn_button;
    [SerializeField] private GameObject vibration_on;
    [SerializeField] private GameObject vibration_off;
    [SerializeField] private GameObject SettingPanel;


   public void soundOff()
    {
        soundOnn_button.SetActive(false);
        soundOff_button.SetActive(true);
    }

    public void soundOn()
    {
        soundOff_button.SetActive(false);
        soundOnn_button.SetActive(true);
    }

    public void vibrationOn()
    {
        vibration_off.SetActive(false);
        vibration_on.SetActive(true);
        
    }

   public void vibrationOff()
    {
        vibration_on.SetActive(false);
        vibration_off.SetActive(true);
    }

    public void closeBut()
    {
        SettingPanel.SetActive(false);
    }

}
