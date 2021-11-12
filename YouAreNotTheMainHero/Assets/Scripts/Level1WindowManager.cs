using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1WindowManager : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        //Menu.active = false; 
    }
    public void ShowMenu()
    {
        Menu.active = true;
    }
}
