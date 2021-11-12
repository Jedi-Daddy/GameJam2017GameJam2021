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

    private int currentLevel = 0;

    void Start()
    {
        //Scene main = SceneManager.GetSceneByName("main");
        //SceneManager.SetActiveScene(main);

        Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowLevel1;
        //Intro.active = true;
        //Logo.active = false;
        //Loading.active = false;
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
        Intro.active = false;
        Logo.active = true;
    }

    public void ShowLogo(VideoPlayer vp)
    {
        vp.Stop();
        Intro.SetActive(false);
        Logo.SetActive(true);
    }

    public void ShowLevel1()
    {
        Logo.active = false;
        currentLevel = 1;
        LoadSceneMain("level1");
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

    public void LoadSceneMain(string sceneName)
    {
        Loading.active = true;
        var loadingVideo = Loading.GetComponentInChildren<VideoPlayer>();
        var texture = new RenderTexture(1920, 1080, 1);
        loadingVideo.targetTexture = texture;
        loadingVideo.transform.parent.GetComponentInChildren<RawImage>().texture = texture;
        loadingVideo.Play();
        StartCoroutine(LoadScene(sceneName));
        loadingVideo.Stop();
        Loading.active = false;
    }

    public IEnumerator LoadScene(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync("level1");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
