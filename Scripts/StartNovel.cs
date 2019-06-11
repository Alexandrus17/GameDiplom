using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class StartNovel : MonoBehaviour {


    /*
     Уровни:
     1-3 - Кофе
     4-6 - Еда
     7-9 - Холодильник
     10-12 - Гигиена
     13-15 - Сон
     16-18 - Компютер
    */
    public Flowchart flowch;

    public static StartNovel main;

    public GameObject FinishButton;

    void Awake()
    {
        main = this;
    }


    public void InGostinaya()
    {
        flowch.SendFungusMessage("InGostin");
    }

    public void MessQuit()
    {
        flowch.SendFungusMessage("Quit");
    }

    public void MessInMainMenu()
    {
        flowch.SendFungusMessage("InMainMenu");
    }

    public int GetLevelEnd()
    {
        Debug.Log("Выход левел " + LevelProfile.main.level);
        return LevelProfile.main.level;
    }

    public void MesEndOptions()
    {
        flowch.SendFungusMessage("EndOptions");
    }

    public void MesEndOptionsWithout()
    {
        flowch.SendFungusMessage("EndOptionsWithout");
    }

    public void MesCompMinus()
    {
        flowch.SendFungusMessage("CopmMinus");
    }


    public void Fin_Activ(bool Activ)
    {
        FinishButton.GetComponentInChildren<Text>().text = Lean.Localization.LeanLocalization.GetTranslationText("Match/ScoreSt1");
        FinishButton.SetActive(Activ);
    }

    public void SetLenguage(string Lang)
    {
        flowch.SetStringVariable("Language", Lang);
        flowch.SendFungusMessage("Setlanguage");
    }
}
