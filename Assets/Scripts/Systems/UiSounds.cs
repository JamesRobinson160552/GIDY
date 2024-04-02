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
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }
}
