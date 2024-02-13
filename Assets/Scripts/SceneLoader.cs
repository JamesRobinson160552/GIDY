//Contains a public method to change between scenes

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("ListScene");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}

