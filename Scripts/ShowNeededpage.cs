using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ShowNeededpage : MonoBehaviour
{
    public int Room;
    public static ShowNeededpage main;

    public Clickable2D[] Allclickable2D;
    /*
1 - Главное меню
2 - Госинная
3 - Кухня
4 - Ванная
5 - Спальня
6 - Кабинет
7 - Поле три в ряд
    */

        public void Cliked2D (bool ckik)
    {
        foreach (Clickable2D clik in Allclickable2D)
        {
            clik.FunclickEnabled(ckik);
        }
    }


    private void Awake()
    {
        main = this;
    }
    public void SetRoom(int page)
    {
        Room = page;
    }

    public int GetRoom()
    {
        return Room;
    }

    public void HideAll()
    {
        UIAssistant.main.ShowPage("Navel");
        Debug.Log("HideALL");
    }


    public void PageShow ()
    {
        switch (Room)
        {
            case 1:
                UIAssistant.main.ShowPage("MainMenu");
                Debug.Log("MainMenu");
                break;
            case 2:
                UIAssistant.main.ShowPage("Gostinaya");
                Debug.Log("Gostinaya");
                break;
            case 3:
                UIAssistant.main.ShowPage("Kitchen");
                Debug.Log("Kitchen");
                break;
            case 4:
                UIAssistant.main.ShowPage("Batch");
                Debug.Log("Batch");
                break;
            case 5:
                UIAssistant.main.ShowPage("Sleep");
                Debug.Log("Sleep");
                break;
            case 6:
                UIAssistant.main.ShowPage("Cabinet");
                Debug.Log("Cabinet");
                break;
            case 7:
                UIAssistant.main.ShowPage("Field");
                Debug.Log("Field");
                break;
            default:
                Debug.LogError("Ошибка смены комнаты");
                break;
        }
    }

}
