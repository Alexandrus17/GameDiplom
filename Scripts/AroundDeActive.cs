using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundDeActive : MonoBehaviour
{

    public GameObject Multi;
    public GameObject Coffeemak;
    public GameObject IceBox;
    public GameObject Shower;
    public GameObject Bed;
    public GameObject Comput;

    public GameObject GOMulti;
    public GameObject GOCoffeemak;
    public GameObject GOIceBox;
    public GameObject GOShower;
    public GameObject GOBed;
    public GameObject GOComput;

    public GameObject __SPASE__;

    public GameObject Gostin;
    public GameObject Kithen;
    public GameObject Batch;
    public GameObject Sleep;
    public GameObject Cabinet;

    public void DeActivRoom()
    {
        Gostin.SetActive(false);
        Kithen.SetActive(false);
        Batch.SetActive(false);
        Sleep.SetActive(false);
        Cabinet.SetActive(false);
    }


    public void DeActiv ()
    {
        Multi.SetActive(false); 
        Coffeemak.SetActive(false); 
        IceBox.SetActive(false); 
        Shower.SetActive(false); 
        Bed.SetActive(false); 
        Comput.SetActive(false);
        Debug.Log("DeActiv: " + false);
    }

    public void ActivGO(bool act)
    {
        GOMulti.SetActive(act);
        GOCoffeemak.SetActive(act);
        GOIceBox.SetActive(act);
        GOShower.SetActive(act);
        GOBed.SetActive(act);
        GOComput.SetActive(act);
    }

}
