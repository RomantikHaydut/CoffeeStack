using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioClip coffeeFillSound;
    [SerializeField] private AudioClip doorSound; // Sleeve
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip upgradeSound;
    private AudioSource audioSource;
    private float volume = 1f;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayCoffeeFillSound()
    {
        PlaySound(coffeeFillSound);
    }

    public void PlayDoorSoundSound()
    {
        PlaySound(doorSound);
    }

    public void PlayCollectSoundSound()
    {
        PlaySound(collectSound);
    }

    public void PlayUpgradeSoundSound()
    {
        PlaySound(upgradeSound);
    }

    public void SoundOn()
    {
        volume = 1f;
    }
    public void SoundOff()
    {
        volume = 0f;
    }

}