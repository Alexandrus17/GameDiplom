﻿using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepTime : MonoBehaviour
{
    public Text TexSleep;
    public Slider SliderSleep;
    public Button BtnSave;
    public Button BtnSleep;
    

    private int TimeSleep;
    private void OnEnable()
    {
        SliderSleep.value = 0;
    }

    public void ChangeSlider ()
    {
        int DeltaSleep = Mathf.Abs (SessionAssistant.main.TimeHour - 10);
        if (SliderSleep.value >= DeltaSleep)
            SliderSleep.value = DeltaSleep;
        TexSleep.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/InBedSleep"), SliderSleep.value, (int)(SliderSleep.value * Parametrs.main.AddEnergy));
    }

    public void SleepFun ()
    {
        BtnSave.interactable=false;
        BtnSleep.interactable = false;
        SliderSleep.interactable = false;
        TimeSleep = (int)SliderSleep.value;
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep ()
    {
        for (int sleep = (int)SliderSleep.value-1; sleep >= 0; sleep--)
        {
            for (int i = 1; i <= 12; i++)
            {
                yield return new WaitForSeconds(0.1f);
                SessionAssistant.main.TimeMinute += 5;
            }
            SliderSleep.value = sleep;
            SessionAssistant.main.energy += Parametrs.main.AddEnergy;
            SessionAssistant.main.eat--;
            SessionAssistant.main.hygiene--;
            SessionAssistant.main.coffee -= 2;
            if (SessionAssistant.main.eat <= 0 || SessionAssistant.main.energy <= 0 || SessionAssistant.main.hygiene <= 0 || SessionAssistant.main.coffee <= 0)
            {
                AudioAssistant.Shot("YouLose");
                UIAssistant.main.ShowPage("YouLoseGame");
                yield return 0;
            }

        }


        yield return new WaitForSeconds(0.5f);
        if (Parametrs.main.NewDay(TimeSleep))
        {
            UIAssistant.main.ShowPage("PayRent");
        }
        else
        {
            StartNovel.main.MesEndOptions();
        }

        SliderSleep.interactable = true;
        BtnSave.interactable = true;
        BtnSleep.interactable = true;

        yield return 0;
    }

}
