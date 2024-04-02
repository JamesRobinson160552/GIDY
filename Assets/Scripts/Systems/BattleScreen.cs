using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleScreen : MonoBehaviour
{
    [SerializeField] Button startBattleButton;
    [SerializeField] TextMeshProUGUI countdown;
    bool canBattle;

    // Start is called before the first frame update
    void Start()
    {
        canBattle = PlayerPrefs.GetInt("canBattle", 0) == 1 ? true : false;
        if (Timer.i.newDay)
        {
            canBattle = true;
            PlayerPrefs.SetInt("canBattle", 1);
        }

        if (canBattle)
        {
            startBattleButton.interactable = true;
            countdown.text = "Ready!";
        }
        else
        {
            startBattleButton.interactable = false;
            countdown.text = Timer.i.timeToNextDay.ToString();
        }
    }

    public void Battle()
    {
        if (canBattle)
        {
            canBattle = false;
            PlayerPrefs.SetInt("canBattle", 0);
            SceneManager.LoadScene("BattleScene");
        }
    }

    void FixedUpdate()
    {
        if (!canBattle)
        {
            countdown.text = Timer.i.timeToNextDayString;
        }
    }
}
