using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticMenu : MonoBehaviour
{
    public Text Days;
    public Text Wins;
    public Text Money;
    public Text MoneyMinus;
    public Text MoneyProfit;
    public Text PayRent;
    public Text Nalog;
    public Text NDS;
    public Text PriceIncrise;
    public Text RentIncrise;

    private void OnEnable()
    {
        Days.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticDays"), SessionAssistant.main.LifeDays); 
        Wins.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticWins"), Parametrs.main.GameWins); 
        Money.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticMoney"), Parametrs.main.MoneyProfit); 
        MoneyMinus.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticMoneyMinus"), Parametrs.main.MoneyOutgo); 
        MoneyProfit.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticMoneyProfit"), (int)(Parametrs.main.AddMoney * Parametrs.main.Nalogi * Parametrs.main.IncriseMoney));
        PayRent.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticPayRent"), (int)(Parametrs.main.Rentincrease * Parametrs.main.Rent));
        Nalog.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticNalog"), (100 - (int)(Parametrs.main.Nalogi * 100)));
        NDS.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticNDS"), ((int)(Parametrs.main.NDS * 100) - 100));
        PriceIncrise.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticPriceIncrise"), ((int)(Parametrs.main.Priceincrease * 100) - 100));
        RentIncrise.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/StatisticRentIncrise"), ((int)(Parametrs.main.Rentincrease * 100) - 100));
    }

}
