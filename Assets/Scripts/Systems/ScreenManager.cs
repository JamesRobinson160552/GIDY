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
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(1020, 480, false);
        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        SceneManager.LoadScene("BattleScene");
    }
}
