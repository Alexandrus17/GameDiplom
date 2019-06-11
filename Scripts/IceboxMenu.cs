using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceboxMenu : MonoBehaviour
{
    public Text CountProduct;
    public Text PayProduct;
    public Text CountCoffee;
    public Text PayCoffee;
    public Text TexPriceProduct;
    public Text TexPriceCoffee;

    private void OnEnable()
    {
        CountProduct.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxCountProduct"), Parametrs.main.numproducts);
        PayProduct.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxPayProduct"), Parametrs.main.NumProductPay);
        CountCoffee.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxCountCoffee"), Parametrs.main.numcoffee);
        PayCoffee.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxPayCoffee"), Parametrs.main.NumProductPay);
        TexPriceProduct.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxTexPriceProduct"), (int)(Parametrs.main.PriceProduct * Parametrs.main.Priceincrease * Parametrs.main.NDS));
        TexPriceCoffee.text = System.String.Format(LeanLocalization.GetTranslationText("Novel/IceBoxTexPriceCoffee"), (int)(Parametrs.main.PriceCoffee * Parametrs.main.Priceincrease * Parametrs.main.NDS));
    }
}
