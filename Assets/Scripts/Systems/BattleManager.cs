using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject attackButtons;
    [SerializeField] private GameObject itemsMenu;
    private float playerBlockModifier = 0;

    public void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        Debug.Log("Called Start player turn");
        LightButtons();
    }

    public void EndPlayerTurn()
    {
        ResetMenu();
        EnemyAttack(enemy.preferredAttackType);
    }

    private void ResetMenu()
    {
        attackButtons.SetActive(false);
        itemsMenu.SetActive(false);
        mainButtons.SetActive(true); 
        DimButtons();
    }

    private void DimButtons()
    {
        foreach (Transform button in  mainButtons.transform)
        {
            button.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private void LightButtons()
    {
        foreach (Transform button in  mainButtons.transform)
        {
            button.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Run()
    {
        Debug.Log("Running!!");
        SceneManager.LoadScene("ListScene");
        Screen.orientation = ScreenOrientation.Portrait;
    }

    //2 is light, 3 is heavy, 4 is magic
    public void EnemyAttack(int attackType)
    {
        if (!enemy.isDead)
        {
            Debug.Log("Enemy Attacking");
            StartCoroutine(enemy.PlayAttackAnimation(attackType));
            int damage = Mathf.FloorToInt(enemy.totalStats[attackType] * playerBlockModifier);
            player.TakeDamage(damage);
            playerBlockModifier = 1;
            StartPlayerTurn();
        }
    }

    public void PlayerAttack(int attackType)
    {
        Debug.Log("player Attacking");
        StartCoroutine(player.PlayAttackAnimation(attackType));
        int damage = player.totalStats[attackType];
        enemy.TakeDamage(damage);
        EndPlayerTurn();
    }

    public void PlayerBlock()
    {
        playerBlockModifier = 0.25f;
        EndPlayerTurn();
    }

    public void EndBattle(string deadCharacter)
    {
        SceneManager.LoadScene("ListScene");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
