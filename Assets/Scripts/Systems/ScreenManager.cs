//Public methods to change screens or start a batte, to be called by buttons

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    public void ChangeScreen(GameObject screenToShow)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
        screenToShow.SetActive(true);
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
