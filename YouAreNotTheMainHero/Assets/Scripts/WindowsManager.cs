using System.Collections;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using UnityEngine.UI;
using Assets.Scripts.ui;

public class WindowsManager : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Logo;
    public GameObject Loading;
    public GameObject GameLogoBackground;
    public AudioSource Source;

    public bool levelCanLoad;

    void Start()
    {
        Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += LoadSceneMain;
        Intro.SetActive(true);
        Logo.SetActive(false);
        Loading.SetActive(false);
        GameLogoBackground.SetActive(false);
        var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
        introVideo.Play();
        introVideo.loopPointReached += ShowGameIntro;
    }

    public void ShowGameIntro(VideoPlayer vp)
    {
        vp.Stop();
        //PreloaderAnimator.Instance.Play("Start_Level");

        StartCoroutine(FadeIn(Source, 0.7f));
        Intro.SetActive(false);
        Logo.SetActive(true);
        var gameIntro = Logo.GetComponentInChildren<VideoPlayer>();
        var texture = new RenderTexture(1920, 1080, 1);
        gameIntro.targetTexture = texture;
        gameIntro.transform.parent.GetComponentInChildren<RawImage>().texture = texture;

        gameIntro.Play();
        gameIntro.loopPointReached += ShowLogo;
    }

    public void ShowLogo(VideoPlayer vp)
    {
        vp.Stop();
        GameLogoBackground.SetActive(true);
        //Intro.SetActive(false);
        //Logo.SetActive(true);
    }

    public void LoadSceneMain()
    {
        //PreloaderAnimator.Instance.Play("Inbetween");
        var videoFinishIsSet = false;
        Logo.SetActive(false);
        Loading.SetActive(true);
        var loadingVideo = Loading.GetComponentInChildren<VideoPlayer>();
        var texture = new RenderTexture(1920, 1080, 1);
        loadingVideo.targetTexture = texture;
        loadingVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;
        loadingVideo.Play();
        StartCoroutine(LoadScene(loadingVideo, videoFinishIsSet));
    }
    public void FinishLoading(VideoPlayer vp)
    {
        vp.Stop();
        StartCoroutine(FadeOut(Source, 0.5f));
        //PreloaderAnimator.Instance.Play("Inbetween");
    }

    public IEnumerator LoadScene(VideoPlayer vp, bool videoFinishIsSet)
    {
        var asyncLoad = SceneManager.LoadSceneAsync("level1");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (!videoFinishIsSet)
                {
                    vp.loopPointReached += FinishLoading;
                    videoFinishIsSet = true;
                }
                if (levelCanLoad)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        levelCanLoad = true;
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
