using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    UnityEngine.Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;
    bool isFullScreen = false;
    public Toggle FullScreen;
    public Slider Audio;
    public Slider Effects;

    public Dropdown Lang;

    /*
    public int iFullScreen
    {
        get
        {
            if (!PlayerPrefs.HasKey("FullScreen"))
                return 0;
            return PlayerPrefs.GetInt("FullScreen");
        }
        set
        {
            PlayerPrefs.SetInt("FullScreen", value);
        }
    }
    */

    void OnApplicationQuit()
    {
        /*
        PlayerPrefs.SetInt("Screenmanager Resolution Width", 800);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", 600);
       // PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 0);
        PlayerPrefs.SetInt("Screenmanager Fullscreen mode", 3);
        */
    }
    int indexRes;
    int ind;
    public void Awake()
    {
         rsl = Screen.resolutions.Select(resolution => new UnityEngine.Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutions = new List<string>();
        indexRes = 0;
        ind = 0;
        foreach (var i in rsl)
        {
                resolutions.Add(i.width + "x" + i.height);
            if (PlayerPrefs.GetInt("Screenmanager Resolution Width", 0) == i.width)
                indexRes = ind;
            ind++;
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
    }

    private void Start()
    {
        // Синхронизация настроек и то, что видно в настройках
        isFullScreen = Screen.fullScreen;
        if (isFullScreen)
        {
            Screen.fullScreen = false;  // потому что дальше срабатывает isOn, который снова переделывает в фулскрин
            isFullScreen = false;
            FullScreen.isOn = true;
        }

        dropdown.value = indexRes;

        Audio.value = AudioAssistant.main.musicVolume;
        Effects.value = AudioAssistant.main.sfxVolume;
        Lang.value = PlayerPrefs.GetInt("SetLanguage", 1);  // возвращает значения этого ключа, если ключа нет, то возвращает 1
        SelectLanguage(PlayerPrefs.GetInt("SetLanguage", 1));
    }

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    //    iFullScreen = isFullScreen ? 1 : 0;
    }

    public void ChengeResolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
        Debug.Log("ChangResol");
    }

    public void SelectLanguage(int var)
    {

        switch (var)
        {
            case 0:
                Lean.Localization.LeanLocalization.CurrentLanguage = "Russian";
                StartNovel.main.SetLenguage("Standard");
                PlayerPrefs.SetInt("SetLanguage", 0);
                break;
            case 1:
                Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                StartNovel.main.SetLenguage("Eng");
                PlayerPrefs.SetInt("SetLanguage", 1);
                break;
            default:
                StartNovel.main.SetLenguage("Eng");
                Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                break;
        }
    }



}


