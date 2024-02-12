using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
