using Assets.Scripts;
using Assets.Scripts.ui;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Level1Ui;
    public GameObject MenuButton;
    public GameObject InfoButton;
    public GameObject InfoUi;
    public Text EnemyDestroyTest;
    public Text MenuEnemyDestroyTest;

    public long EnemyDestroyed = 0;

    void Start()
    {
        Menu.SetActive(false);
        InfoUi.SetActive(false);
        Menu.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        Menu.GetComponentInChildren<ButtonScriptRestart>().ActionDelegate += RestartLevel;
        Menu.GetComponentInChildren<ButtonScriptHideMenu>().ActionDelegate += HideMenu;
        InfoUi.GetComponentInChildren<ButtonScriptHideMenu>().ActionDelegate += HideInfo;
        MenuButton.GetComponent<ButtonScript>().ActionDelegate += ShowMenu;
        InfoButton.GetComponent<ButtonScript>().ActionDelegate += ShowInfo;
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
    }

    private void Update()
    {
        EnemyDestroyTest.text = EnemyDestroyed.ToString();
    }

    private void OnEnable()
    {
        EventDispatcher.OnGameOver += ShowGameOver;
        EventDispatcher.OnEnemyDiedByShadow += UpdateEnemyDestroyedCount;
    }

    private void OnDisable()
    {
        EventDispatcher.OnGameOver -= ShowGameOver;
        EventDispatcher.OnEnemyDiedByShadow -= UpdateEnemyDestroyedCount;
    }
    public void ShowInfo()
    {
        Time.timeScale = 0f;
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = false;
        InfoUi.SetActive(true);
    }

    public void HideInfo()
    {
        InfoUi.SetActive(false);
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
        Time.timeScale = 1f;
    }
    public void ShowMenu()
    {
        Time.timeScale = 0f;
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = false;
        MenuEnemyDestroyTest.text = EnemyDestroyed.ToString();
        Menu.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.SetActive(false);
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("level1");
        Time.timeScale = 1f;
    }

    public void ShowGameOver(object sender, EventArgs args)
    {
        ShowMenu();
    }

    public void UpdateEnemyDestroyedCount(object sender, EventArgs args)
    {
        EnemyDestroyed++;
    }
}
