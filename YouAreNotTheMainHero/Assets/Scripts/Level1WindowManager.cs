using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        //Menu.active = false;
        Menu.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
    }
    public void ShowMenu()
    {
        Menu.active = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
