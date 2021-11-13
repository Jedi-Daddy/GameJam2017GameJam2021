using Assets.Scripts;
using Assets.Scripts.ui;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Level1Ui;
    public GameObject MenuButton;
    public GameObject InfoButton;
    public GameObject InfoUi;
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
    public void ShowInfo()
    {
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = false;
        InfoUi.SetActive(true);
    }

    public void HideInfo()
    {
        InfoUi.SetActive(false);
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
    }
    public void ShowMenu()
    {
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = false;
        Menu.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.SetActive(false);
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("level1");
    }
}
