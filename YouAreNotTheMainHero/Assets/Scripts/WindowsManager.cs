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
    public GameObject Level1Ui;


    private int currentLevel = 0;

    void Start()
    {
        //Scene main = SceneManager.GetSceneByName("main");
        //SceneManager.SetActiveScene(main);

        Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowLoading;
        Level1Ui = GameObject.Find("Level1Ui");
        //Intro.active = true;
        //Logo.active = false;
        //Loading.active = false;
        //Level1Ui.active = false;
        //var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
        //var texture = new RenderTexture(1920, 1080, 1);
        //introVideo.targetTexture = texture;
        //introVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;
        //introVideo.Play();
        //introVideo.loopPointReached += ShowLogo;

        ShowLogoTest();
    }

    //public void ShowLogo(VideoPlayer vp)
    //{
    //    vp.Stop();
    //    Intro.active = false;
    //    Logo.active = true;
    //    var loadingVideo = Logo.GetComponentInChildren<VideoPlayer>();
    //    //var texture = new RenderTexture(1920, 1080, 1);
    //    //loadingVideo.targetTexture = texture;
    //    //loadingVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;

    //    loadingVideo.Play();
    //    loadingVideo.loopPointReached += ShowLevel1;
    //}

    //public void ShowLevel1(VideoPlayer vp)
    //{
    //    vp.Stop();
    //    Logo.active = false;
    //    Level1.active = true;
    //    currentLevel = 1;
    //}

    public void ShowLogoTest()
    {
        Intro.SetActive(false);
        Logo.SetActive(true);
    }

    public void ShowLogo(VideoPlayer vp)
    {
        vp.Stop();
        Intro.SetActive(false);
        Logo.SetActive(true);
    }

    //public void NextLevel()
    //{
    //    switch (currentLevel)
    //    {
    //        case 1:
    //            ShowIntroLevel2();
    //            return;
    //        case 2:
    //            ShowIntroLevel3();
    //            return;
    //        case 3:
    //            ShowIntroLevel4();
    //            return;
    //        case 4:
    //            ShowEnd();
    //            return;
    //    }
    //}

    public void ShowLoading()
    {
        Logo.SetActive(false);
        currentLevel = 1;
        Loading.SetActive(true);
        var loadingVideo = Loading.GetComponentInChildren<VideoPlayer>();
        //var texture = new RenderTexture(1920, 1080, 1);
        //loadingVideo.targetTexture = texture;
        //loadingVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;
        loadingVideo.Play();
        loadingVideo.loopPointReached += ShowLevel1;
    }

    public void ShowLevel1(VideoPlayer vp)
    {
        vp.Stop();
        Loading.SetActive(false);
        Level1Ui.SetActive(true);
    }

    public void LoadSceneMain()
    {
        Logo.SetActive(false);
        currentLevel = 1;
        Loading.SetActive(true);
        var loadingVideo = Loading.GetComponentInChildren<VideoPlayer>();
        loadingVideo.Play();
        StartCoroutine(LoadScene());
        loadingVideo.Stop();
        Loading.SetActive(false);
        Level1Ui.SetActive(true);
    }

    public IEnumerator LoadScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("level1");
        //asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
