using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSounds : MonoBehaviour
{
    public static BattleSounds i { get; private set; }

    public AudioSource audioSource;

    public AudioClip[] lightAttacks;

    public AudioClip[] heavyAttacks;
    public AudioClip[] magicAttacks;
    public AudioClip[] monsterDeaths;
    public AudioClip[] playerDeath;
    public AudioClip[] monsterSpawns;

    void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(480, 1020, false);
        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    public void PlaySound(int type)
    {
        switch (type)
        {
            case 0:
                audioSource.PlayOneShot(lightAttacks[Random.Range(0, lightAttacks.Length)]);
                break;
            case 1:
                audioSource.PlayOneShot(heavyAttacks[Random.Range(0, heavyAttacks.Length)]);
                break;
            case 2:
                audioSource.PlayOneShot(magicAttacks[Random.Range(0, magicAttacks.Length)]);
                break;
            case 3:
                audioSource.PlayOneShot(monsterDeaths[Random.Range(0, monsterDeaths.Length)]);
                break;
            case 4:
                audioSource.PlayOneShot(playerDeath[Random.Range(0, playerDeath.Length)]);
                break;
            case 5:
                audioSource.PlayOneShot(monsterSpawns[Random.Range(0, monsterSpawns.Length)]);
                break;
            default:
                break;
        }
    }
}
