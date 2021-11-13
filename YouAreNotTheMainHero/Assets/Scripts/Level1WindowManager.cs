using Assets.Scripts;
using Assets.Scripts.ui;
using UnityEngine;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Level1Ui;
    public GameObject MenuButton;
    void Start()
    {
        Menu.SetActive(false);
        Menu.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        Menu.GetComponentInChildren<ButtonScriptRestart>().ActionDelegate += RestartLevel;
        Menu.GetComponentInChildren<ButtonScriptHideMenu>().ActionDelegate += HideMenu;
        MenuButton.GetComponent<ButtonScript>().ActionDelegate += ShowMenu;
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
    }
}
