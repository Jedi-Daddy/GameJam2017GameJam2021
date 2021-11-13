using System.Collections;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using UnityEngine.UI;

public class WindowsManager : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Logo;
    public GameObject Loading;
    public GameObject GameLogoBackground;

    public bool loadingStopPlay;

    private int currentLevel = 0;

    void Start()
    {
        //Scene main = SceneManager.GetSceneByName("main");
        //SceneManager.SetActiveScene(main);

        Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += LoadSceneMain;
        Intro.SetActive(true);
        Logo.SetActive(false);
        Loading.SetActive(false);
        GameLogoBackground.SetActive(false);
        var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
        //var texture = new RenderTexture(1920, 1080, 1);
        //introVideo.targetTexture = texture;
        //introVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;
        introVideo.Play();
        introVideo.loopPointReached += ShowGameIntro;

        //ShowLogoTest();
    }

    public void ShowGameIntro(VideoPlayer vp)
    {
        vp.Stop();
        Intro.SetActive(false);
        Logo.SetActive(true);
        var gameIntro = Logo.GetComponentInChildren<VideoPlayer>();
        var texture = new RenderTexture(1920, 1080, 1);
        gameIntro.targetTexture = texture;
        gameIntro.transform.parent.GetComponentInChildren<RawImage>().texture = texture;

        gameIntro.Play();
        gameIntro.loopPointReached += ShowLogo;
    }

    //public void ShowLogoTest()
    //{
    //    Intro.SetActive(false);
    //    Logo.SetActive(true);
    //}

    public void ShowLogo(VideoPlayer vp)
    {
        vp.Stop();
        GameLogoBackground.SetActive(true);
        //Intro.SetActive(false);
        //Logo.SetActive(true);
    }

    public void LoadSceneMain()
    {
        var videoFinishIsSet = false;
        Logo.SetActive(false);
        currentLevel = 1;
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
        loadingStopPlay = true;
        vp.Stop();
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
                if (loadingStopPlay)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

}
