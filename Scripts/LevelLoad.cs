using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour   
{   
    public int NumbOfLevel;
    public float Testf;
    int Testi;
    System.Random r = new System.Random();

    public float IncriseNalogiInOneMin;
    public float IncriseNalogiInOneMax;

    public GameObject DebugGO;

    public Text text;

    public bool activv = true;

    public void NextLevell ()
    {
        SessionAssistant.main.coffeevarka = true;
        SessionAssistant.main.multvarka = true;
        SessionAssistant.main.icebox = true;
        SessionAssistant.main.shower = true;
        SessionAssistant.main.bed = true;
        SessionAssistant.main.computer = true;

        ChangeBG.main.UpdateStatusDevice();
        NumbOfLevel++;
        if  (NumbOfLevel>21)
        {
            NumbOfLevel = 1;
        }
    }


    public void DebugPlus()
    {
        SessionAssistant.main.eat++;
        SessionAssistant.main.energy++;
        SessionAssistant.main.hygiene++;
        SessionAssistant.main.coffee++;
        
        SessionAssistant.main.TimeMinute += 50;
    }

    public void DebugMinus()
    {
        SessionAssistant.main.eat--;
        SessionAssistant.main.energy--;
        SessionAssistant.main.hygiene--;
        SessionAssistant.main.coffee--;
        SessionAssistant.main.money -= 50;
        
    }

    
    void Update () {
        text.text = "lev = "+NumbOfLevel;
        if (Input.GetKeyDown(KeyCode.J))
        {
            activv = !activv;
            DebugGO.SetActive(activv);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Level.LoadMyLev(NumbOfLevel);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {

        }
    }
}
