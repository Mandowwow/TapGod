using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip buttonPressSound;
    public AudioClip timerEndSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayButton.OnButtonPressed += PlayButtonPressSound;
    }

    private void OnDestroy()
    {
        PlayButton.OnButtonPressed -= PlayButtonPressSound;
    }

    private void PlayButtonPressSound()
    {
        if (buttonPressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonPressSound);
        }
    }

    public void PlayTimerEndSound()
    {
        if (timerEndSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(timerEndSound);
        }
    }
}

