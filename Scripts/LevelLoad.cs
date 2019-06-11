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
        MatchThree.main.coffeevarka = true;
        MatchThree.main.multvarka = true;
        MatchThree.main.icebox = true;
        MatchThree.main.shower = true;
        MatchThree.main.bed = true;
        MatchThree.main.computer = true;

        ChangeBG.main.UpdateStatusDevice();
        NumbOfLevel++;
        if  (NumbOfLevel>21)
        {
            NumbOfLevel = 1;
        }
    }


    public void DebugPlus()
    {
        MatchThree.main.eat++;
        MatchThree.main.energy++;
        MatchThree.main.hygiene++;
        MatchThree.main.coffee++;
        
        MatchThree.main.TimeMinute += 50;
    }

    public void DebugMinus()
    {
        MatchThree.main.eat--;
        MatchThree.main.energy--;
        MatchThree.main.hygiene--;
        MatchThree.main.coffee--;
        MatchThree.main.money -= 50;
        
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
