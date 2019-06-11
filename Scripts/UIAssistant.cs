using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class UIAssistant : MonoBehaviour {

    public static UIAssistant main;
    public List<Transform> UImodules = new List<Transform>();

    public delegate void Action();
    public static Action onScreenResize = delegate {};
    public static Action<string> onShowPage = delegate {};
    Vector2 screenSize;

    public List<CPanel> panels = new List<CPanel>(); 
    public List<Page> pages = new List<Page>(); 

    private string currentPage; 
    private string previousPage; 

    void Start() {
        ArraysConvertation(); 
        Page defaultPage = GetDefaultPage();
        if (defaultPage != null)
            ShowPage(defaultPage, true); 
    }

    void Awake() {
        main = this;
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update() {
        if (screenSize != new Vector2(Screen.width, Screen.height)) {
            screenSize = new Vector2(Screen.width, Screen.height);
            onScreenResize.Invoke();
        }
    }

    
    public void ArraysConvertation() {
        
        panels = new List<CPanel>();
        panels.AddRange(GetComponentsInChildren<CPanel>(true));
        foreach (Transform module in UImodules)
            panels.AddRange(module.GetComponentsInChildren<CPanel>(true));
        if (Application.isEditor)
            panels.Sort((CPanel a, CPanel b) => {
                return string.Compare(a.name, b.name);
            });
    }

    public void ShowPage(Page page, bool immediate = false) {
        
        if (CPanel.uiAnimation > 0)
            return;
           

        if (currentPage == page.name)
            return;

        if (pages == null)
            return;

        previousPage = currentPage;
        currentPage = page.name;


        foreach (CPanel panel in panels) {
            if (page.panels.Contains(panel))
                panel.SetVisible(true, immediate);
            else
                if (!page.ignoring_panels.Contains(panel) && !panel.freez)
                    panel.SetVisible(false, immediate);
        }
        
        onShowPage.Invoke(page.name);

        if (page.soundtrack != "-")
        {
            if (page.soundtrack != AudioAssistant.main.currentTrack)
            {
                if (page.name=="Field")
                {
                    if (LevelProfile.main.level <= 11)
                    {
                        if ("Level" + LevelProfile.main.level != AudioAssistant.main.currentTrack)
                        {
                            AudioAssistant.main.PlayMusic("Level" + LevelProfile.main.level);
                            Debug.Log("Trak = " + "Level" + LevelProfile.main.level);
                        }
                    }
                    else
                    {
                       if ("Level" + (LevelProfile.main.level - 11) != AudioAssistant.main.currentTrack)
                        {
                            AudioAssistant.main.PlayMusic("Level" + (LevelProfile.main.level - 11));
                            Debug.Log("Trak = " + "Level" + (LevelProfile.main.level - 11));
                        }
                    }
                }
                else
                AudioAssistant.main.PlayMusic(page.soundtrack);
            }
        }

        if (page.setTimeScale)
            Time.timeScale = page.timeScale;
    }

    public void ShowPage(string page_name) {
        ShowPage(page_name, false);
    }

    public void ShowPage(string page_name, bool immediate) {
        Page page = pages.Find(x => x.name == page_name);
        if (page != null)
            ShowPage(page, immediate);
    }

    public void FreezPanel(string panel_name, bool value = true) {
        CPanel panel = panels.Find(x => x.name == panel_name);
        if (panel != null)
            panel.freez = value;
    }

    public void SetPanelVisible(string panel_name, bool visible, bool immediate = false) {
        CPanel panel = panels.Find(x => x.name == panel_name);
        if (panel) {
            if (immediate)
                panel.SetVisible(visible, true);
            else
                panel.SetVisible(visible);
        }
    }

    
    public void HideAll() {
        foreach (CPanel panel in panels)
            panel.SetVisible(false);
    }

    
    public void ShowPreviousPage() {
        ShowPage(previousPage);
    }

    public string GetCurrentPage() {
        return currentPage;
    }

    public Page GetDefaultPage() {
        return pages.Find(x => x.default_page);
    }

    
    [System.Serializable]
    public class Page {
        public string name; 
        public List<CPanel> panels = new List<CPanel>(); 
        public List<CPanel> ignoring_panels = new List<CPanel>(); 
        public string soundtrack = "-";
        public bool default_page = false;
        public bool setTimeScale = true;
        public float timeScale = 1;
    }
}