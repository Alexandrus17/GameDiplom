using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimUI : MonoBehaviour
{
    public Animator EatAnim;
    public Animator CoffeeAnim;
    public Animator HygAnim;
    public Animator EnergAnim;
    public Animator MoneyAnim;

    // Update is called once per frame
    void Update()
    {
        if (MatchThree.main.eat <= 10)
        {
            EatAnim.SetInteger("Color", 1);
        }
        else if (MatchThree.main.eat <= 30)
        {
            EatAnim.SetInteger("Color", 2);
        }
        else
        {
            EatAnim.SetInteger("Color", 3);
        }

        if (MatchThree.main.coffee <= 10)
        {
            CoffeeAnim.SetInteger("Color", 1);
        }
        else if (MatchThree.main.coffee <= 30)
        {
            CoffeeAnim.SetInteger("Color", 2);
        }
        else
        {
            CoffeeAnim.SetInteger("Color", 3);
        }

        if (MatchThree.main.hygiene <= 10)
        {
            HygAnim.SetInteger("Color", 1);
        }
        else if (MatchThree.main.hygiene <= 30)
        {
            HygAnim.SetInteger("Color", 2);
        }
        else
        {
            HygAnim.SetInteger("Color", 3);
        }

        if (MatchThree.main.energy <= 10)
        {
            EnergAnim.SetInteger("Color", 1);
        }
        else if (MatchThree.main.energy <= 30)
        {
            EnergAnim.SetInteger("Color", 2);
        }
        else
        {
            EnergAnim.SetInteger("Color", 3);
        }

        if (MatchThree.main.money >= (int)(Parametrs.main.Rent * Parametrs.main.Rentincrease))
        {
            MoneyAnim.SetBool("Low", false);
        }
        else
        {
            MoneyAnim.SetBool("Low", true);
        }
    }
}
