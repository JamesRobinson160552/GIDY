using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    public void Run()
    {
        SceneManager.LoadScene("ListScene");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
