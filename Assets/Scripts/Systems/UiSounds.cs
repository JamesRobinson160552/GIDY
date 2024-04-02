using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> sounds;
    public static UiSounds instance;

    void Awake()
    {
        instance = this;
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(480, 1020, false);
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }
}
