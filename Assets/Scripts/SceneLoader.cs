//Contains a public method to change between scenes

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("ListScene");
    }
}

