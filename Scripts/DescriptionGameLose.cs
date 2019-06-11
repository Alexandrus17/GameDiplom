using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionGameLose : MonoBehaviour
{
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        text.text = "";

        string descrition = Parametrs.main.TextGameLose();

        text.text += descrition + " ";
        descrition = System.String.Format(Lean.Localization.LeanLocalization.GetTranslationText("Match/LoseGameDescrip"), SessionAssistant.main.LifeDays);

        text.text += descrition;
    }
}
