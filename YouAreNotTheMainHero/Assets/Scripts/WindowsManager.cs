using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;

public class WindowsManager : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Logo;
    public Scene Level1;
    public GameObject MenuRestart;
    public GameObject MenuNext;
    public GameObject GameEndWin;

    private int currentLevel = 0;

    void Start()
    {
        //    MenuRestart.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowRestart;
        //    MenuRestart.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        //    MenuNext.GetComponentInChildren<ButtonScript>().ActionDelegate += ShowRestart;
        //    MenuNext.GetComponentInChildren<ButtonScriptExit>().ActionDelegate += ExitGame;
        //    MenuNext.GetComponentInChildren<ButtonScriptNext>().ActionDelegate += NextLevel;

        Logo.GetComponentInChildren<ButtonScriptNext>().ActionDelegate += ShowLevel1;
        Intro.active = true;
        var introVideo = Intro.GetComponentInChildren<VideoPlayer>();
        introVideo.Play();
        introVideo.loopPointReached += ShowLogo;
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

    public void ShowLogo(VideoPlayer vp)
    {
        vp.Stop();
        Intro.active = false;
        Logo.active = true;
    }

    public void ShowLevel1()
    {
        Logo.active = false;
        Level1.active = true;
        currentLevel = 1;
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
}
