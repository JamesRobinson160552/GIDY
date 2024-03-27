using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> songs;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        switch (sceneName)
        {
            case "ListScene":
                audioSource.clip = songs[0];
                break;
            case "BattleScene":
                audioSource.clip = songs[1];
                break;
            default:
                audioSource.clip = songs[0];
                break;
        }
        audioSource.Play(); 
    }
}
