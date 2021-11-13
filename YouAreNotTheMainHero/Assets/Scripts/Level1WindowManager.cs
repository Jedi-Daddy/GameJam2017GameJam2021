using Assets.Scripts.ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Level1Ui;
    void Start()
    {
        //Menu.active = false;
        //Menu.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = true;
    }
    public void ShowMenu()
    {
        Menu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
