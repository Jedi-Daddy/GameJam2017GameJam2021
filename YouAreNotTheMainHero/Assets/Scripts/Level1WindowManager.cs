using Assets.Scripts;
using Assets.Scripts.ui;
using System;
using System.Collections;
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
    public GameObject GameOverUi;
    public Text EnemyDestroyTest;
    public Text MenuEnemyDestroyTest;
    public Text GameOverEnemyDestroyTest;

    public int EnemyDestroyed = 0;
    public AudioSource Source;

    void Start()
    {
        StartCoroutine(FadeIn(Source, 0.8f));
        Level1Ui.SetActive(true);
        Menu.SetActive(false);
        InfoUi.SetActive(false);
        GameOverUi.SetActive(false);
        Menu.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        Menu.GetComponentInChildren<ButtonScriptRestart>().ActionDelegate += RestartLevel;
        Menu.GetComponentInChildren<ButtonScriptHideMenu>().ActionDelegate += HideMenu;

        GameOverUi.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        GameOverUi.GetComponentInChildren<ButtonScriptRestart>().ActionDelegate += RestartLevel;

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
        Time.timeScale = 0f;
        Level1Ui.GetComponentInChildren<SunManager>().enableListen = false;
        GameOverEnemyDestroyTest.text = EnemyDestroyed.ToString();
        GameOverUi.SetActive(true);
    }

    public void UpdateEnemyDestroyedCount(object sender, EventArgs args)
    {
        EnemyDestroyed++;
        EventDispatcher.OnScoreUpdated?.Invoke(this, new IntEventArgs(EnemyDestroyed));
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = 1f;
    }
}
