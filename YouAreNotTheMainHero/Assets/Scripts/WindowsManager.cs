using System.Collections;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

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

        Logo.GetComponentInChildren<ButtonScript>().ActionDelegate += LoadSceneMain;
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
    

    public void LoadSceneMain()
    {
        var videoFinishIsSet = false;
        Logo.SetActive(false);
        currentLevel = 1;
        Loading.SetActive(true);
        var loadingVideo = Loading.GetComponentInChildren<VideoPlayer>();
        loadingVideo.Play();
        StartCoroutine(LoadScene(loadingVideo, videoFinishIsSet));
    }
    public void FinishLoading(VideoPlayer vp)
    {
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
                if (!vp.isPlaying)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

}
