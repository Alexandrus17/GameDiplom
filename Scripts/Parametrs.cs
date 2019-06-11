using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using Lean.Localization;

public class Parametrs : MonoBehaviour
{
    //  private Flowchart flowchart;

    //изначальные константы
    private int  AddititionParameters;  
    public int BaseAddParametr;  // на сколько увеличиваются параметры за раз + random
    public int AddEnergy;
    public int AddMoney;  // изначальный зароботок денег
    public int Rent;     // изначальная плата 
    public int PriceProduct;   // изначальная стоймость продуктов
    public int PriceCoffee;   // изначальная стоймость уофе 
    public int NumProductPay;   // сколько продуктов и кофе покупается за 1 раз 
    public float IncriseNalogiInOneMin;  // на сколько увеличивать налоги мин
    public float IncriseNalogiInOneMax;  // на сколько увеличивать налоги макс
    public double IncriseMoneyInOne;  // на сколько увеличивать доход за 1 раз

    public string _____SPASE_____;

    //Менюящиеся параметры
    public int LastNalogDay; //в какой последний день увнличивались налоги
    public double IncriseMoney;  // общее увеличение доходв от компа 1 - изначальное значение
    // Налоги
    public double Nalogi;  // 1 - изначальное значение
    public double NDS; // 1 - изначальное значение
    public double Priceincrease; // 1 - изначальное значение
    public double Rentincrease; // 1 - изначальное значение
    //продукты
    public int numproducts;  // число продуктов в холодильнике
    public int numcoffee;   // число кофе в холодильнике 
    //Statistic
    public int MovesinGame;   // ходов стелано в последней игре
    public int GameWins;    // игр выйграно 
    public int MoneyProfit;  // денег заработано 
    public int MoneyOutgo;  // денег потрачено


    public string _____SPASE2_____;

    public Flowchart flowchart;

    public static Parametrs main;

    public Slider SlEat;
    public Slider SlEnergy;
    public Slider SlCoffee;
    public Slider SlHygiene;


    public Text TeEat;
    public Text TeEnergy;
    public Text TexCoffee;
    public Text TexHygiene;
    public Text TexMoney;

    public Text TexTime;
    public Text TexDays;

    // Status Device
    public Text TexStatusCoffee;
    public Text TexStatusMulti;
    public Text TexStatusIcebox;
    public Text TexStatusShower;
    public Text TexStatusBed;
    public Text TexStatusComputer;

    public string _____SPASE3_____;

    //Nalogi
    public Text TexNameNalog;
    public Text TexDescriptions;
    public Text TexIncrease;
    public Text TexNewNalog;
    public Text TexPayRent;

    public GameObject ContinueBut;

    public Text TexSaveGame;

    bool HaveSaveFile;

    public Animator MoneyAnim;

    private void Awake()
    {
        main = this;
        if (FileBasedPrefs.HasKeyForBool("HaveSaveFile"))
            HaveSaveFile = true;
        else
            HaveSaveFile = false;

        //EatAnim = TeEat.gameObject.GetComponentInParent<Animator>();
    }

    public void Minus()
    {
        MatchThree.main.eat--;
        MatchThree.main.energy--;
        MatchThree.main.hygiene--;
        MatchThree.main.coffee--;
    }
    


    public void NewGame()  //30 переменных
    {
        SteamAchiments.main.TakeAchievement("NEW_GAME");
        MatchThree.main.LifeDays = 1;
        MatchThree.main.TimeHour = 10;
        MatchThree.main.TimeMinute = 0;
        LastNalogDay = 1; //в какой последний день увеличивались налоги

        MatchThree.main.eat = 25;
        MatchThree.main.coffee = 25;
        MatchThree.main.hygiene = 25;
        MatchThree.main.energy = 95;
        MatchThree.main.money = 250;

        MatchThree.main.coffeevarka = true;
        MatchThree.main.multvarka = true;
        MatchThree.main.icebox = true;
        MatchThree.main.shower = true;
        MatchThree.main.bed = true;
        MatchThree.main.computer = false;

        IncriseMoney = 1;  // общее увеличение доходв от компа 
        // Налоги
        Nalogi = 1;
        NDS = 1;
        Priceincrease = 1;
        Rentincrease = 1;
        //продукты
        numproducts = 3;  // число продуктов в холодильнике
        numcoffee = 3;   // число кофе в холодильнике 
                         //Statistic
        MovesinGame = 0;   // ходов стелано в последней игре
        GameWins = 0;    // игр выйграно 
        MoneyProfit = 0;  // денег заработано 
        MoneyOutgo = 0;  // денег потрачено


        //Первый раз в комнате
        flowchart.SetBooleanVariable("FirstKitchen", true);
        flowchart.SetBooleanVariable("FirstShower", true);
        flowchart.SetBooleanVariable("FirstBed", true);
        flowchart.SetBooleanVariable("Firstcabinet", true);

        flowchart.SetBooleanVariable("HaveCompDialog", true);  // показано сообщение со сломаным пк

        ChangeBG.main.UpdateStatusDevice();

    }

    public void LoadGame()  //30 переменных
    {
       // SteamAchiments.main.LockAchiv("ACH_TRAVEL_FAR_ACCUM");
        MatchThree.main.LifeDays = FileBasedPrefs.GetInt("LifeDays");
        MatchThree.main.TimeHour = FileBasedPrefs.GetInt("TimeHour");
        MatchThree.main.TimeMinute = FileBasedPrefs.GetInt("TimeMinute");
        LastNalogDay = FileBasedPrefs.GetInt("LastNalogDay");

        MatchThree.main.eat = FileBasedPrefs.GetInt("eat");
        MatchThree.main.energy = FileBasedPrefs.GetInt("energy");
        MatchThree.main.coffee = FileBasedPrefs.GetInt("coffee");
        MatchThree.main.hygiene = FileBasedPrefs.GetInt("hygiene");
        MatchThree.main.money = FileBasedPrefs.GetInt("money");

        MatchThree.main.coffeevarka = FileBasedPrefs.GetBool("coffeevarka");
        MatchThree.main.multvarka = FileBasedPrefs.GetBool("multvarka");
        MatchThree.main.icebox = FileBasedPrefs.GetBool("icebox");
        MatchThree.main.shower = FileBasedPrefs.GetBool("shower");
        MatchThree.main.bed = FileBasedPrefs.GetBool("bed");
        MatchThree.main.computer = FileBasedPrefs.GetBool("computer");

        flowchart.SetBooleanVariable("FirstKitchen", FileBasedPrefs.GetBool("FirstKitchen"));
        flowchart.SetBooleanVariable("FirstShower", FileBasedPrefs.GetBool("FirstShower"));
        flowchart.SetBooleanVariable("FirstBed", FileBasedPrefs.GetBool("FirstBed"));
        flowchart.SetBooleanVariable("Firstcabinet", FileBasedPrefs.GetBool("Firstcabinet"));

        flowchart.SetBooleanVariable("HaveCompDialog", FileBasedPrefs.GetBool("HaveCompDialog"));  // Показывался ли диалог со сломаным пк

        IncriseMoney = FileBasedPrefs.GetFloat("IncriseMoney");  // общее увеличение доходв от компа 
    // Налоги
    Nalogi = FileBasedPrefs.GetFloat("Nalogi");
    NDS = FileBasedPrefs.GetFloat("NDS");
    Priceincrease = FileBasedPrefs.GetFloat("Priceincrease");
    Rentincrease = FileBasedPrefs.GetFloat("Rentincrease");
        //продукты
    numproducts = FileBasedPrefs.GetInt("numproducts");  // число продуктов в холодильнике
    numcoffee = FileBasedPrefs.GetInt("numcoffee");   // число кофе в холодильнике 
    //Statistic
    MovesinGame = FileBasedPrefs.GetInt("MovesinGame");   // ходов стелано в последней игре
    GameWins = FileBasedPrefs.GetInt("GameWins");    // игр выйграно 
    MoneyProfit = FileBasedPrefs.GetInt("MoneyProfit");  // денег заработано 
    MoneyOutgo = FileBasedPrefs.GetInt("MoneyOutgo");  // денег потрачено

        ChangeBG.main.UpdateStatusDevice();
    }

    public void SaveGame() //30 переменных
    {
        int random = Random.Range(1, 3);
        if (random != 1)
        {
            MatchThree.main.bed = false;
            TexSaveGame.text = LeanLocalization.GetTranslationText("Parametrs/SaveBedBroken");
            ChangeBG.main.UpdateStatusDevice();
            AudioAssistant.Shot("Break");
        }
        else
        {
            TexSaveGame.text = LeanLocalization.GetTranslationText("Parametrs/SaveBedWork");
        }

        FileBasedPrefs.SetInt("LifeDays", MatchThree.main.LifeDays);
        FileBasedPrefs.SetInt("TimeHour", MatchThree.main.TimeHour);
        FileBasedPrefs.SetInt("TimeMinute", MatchThree.main.TimeMinute);
        FileBasedPrefs.SetInt("LastNalogDay", LastNalogDay);

        FileBasedPrefs.SetInt("eat", MatchThree.main.eat);
        FileBasedPrefs.SetInt("energy", MatchThree.main.energy);
        FileBasedPrefs.SetInt("coffee", MatchThree.main.coffee);
        FileBasedPrefs.SetInt("hygiene", MatchThree.main.hygiene);
        FileBasedPrefs.SetInt("money", MatchThree.main.money);

        FileBasedPrefs.SetBool("coffeevarka", MatchThree.main.coffeevarka);
        FileBasedPrefs.SetBool("multvarka", MatchThree.main.multvarka);
        FileBasedPrefs.SetBool("icebox", MatchThree.main.icebox);
        FileBasedPrefs.SetBool("shower", MatchThree.main.shower);
        FileBasedPrefs.SetBool("bed", MatchThree.main.bed);
        FileBasedPrefs.SetBool("computer", MatchThree.main.computer);

        FileBasedPrefs.SetFloat("IncriseMoney", (float)IncriseMoney);

        FileBasedPrefs.SetFloat("Nalogi", (float)Nalogi);
        FileBasedPrefs.SetFloat("NDS", (float)NDS);
        FileBasedPrefs.SetFloat("Priceincrease", (float)Priceincrease);
        FileBasedPrefs.SetFloat("Rentincrease", (float)Rentincrease);

        FileBasedPrefs.SetInt("numproducts", numproducts);
        FileBasedPrefs.SetInt("numcoffee", numcoffee);

        FileBasedPrefs.SetInt("MovesinGame", MovesinGame);
        FileBasedPrefs.SetInt("GameWins", GameWins);
        FileBasedPrefs.SetInt("MoneyProfit", MoneyProfit);
        FileBasedPrefs.SetInt("MoneyOutgo", MoneyOutgo);

        FileBasedPrefs.SetBool("FirstKitchen", flowchart.GetBooleanVariable("FirstKitchen"));
        FileBasedPrefs.SetBool("FirstShower", flowchart.GetBooleanVariable("FirstShower"));
        FileBasedPrefs.SetBool("FirstBed", flowchart.GetBooleanVariable("FirstBed"));
        FileBasedPrefs.SetBool("Firstcabinet", flowchart.GetBooleanVariable("Firstcabinet"));

        FileBasedPrefs.SetBool("HaveCompDialog", flowchart.GetBooleanVariable("HaveCompDialog"));  // Показывался ли диалог со сломаным пк

        //Есть сохранение
        FileBasedPrefs.SetBool("HaveSaveFile", true);
        HaveSaveFile = true;

        UIAssistant.main.ShowPage("CompleteSave");

        Debug.Log(Application.persistentDataPath);
    }

    public void ContinueButtonEneble()
    {
        if (HaveSaveFile)
            ContinueBut.SetActive(true);
        else
            ContinueBut.SetActive(false);
    }


    public string TextGameLose()
    {
        if (MatchThree.main.hygiene <= 0)
        {
            return LeanLocalization.GetTranslationText("Parametrs/DeadHyg");
        }
        if (MatchThree.main.eat <= 0)
        {
            return LeanLocalization.GetTranslationText("Parametrs/DeadEat");
        }
        if (MatchThree.main.energy <= 0)
        {
            return LeanLocalization.GetTranslationText("Parametrs/DeadEner");
        }
        if (MatchThree.main.coffee <= 0)
        {
            return LeanLocalization.GetTranslationText("Parametrs/DeadCoffe");
        }

        return LeanLocalization.GetTranslationText("Parametrs/DeadMoney");
    }

    public string LuckFixDevice()
    {
        GameWins++;
        string text;
        switch ((int)((LevelProfile.main.level - 1) / 3f) + 1)
        {
            case 1:
                MatchThree.main.coffeevarka = true;
                text = LeanLocalization.GetTranslationText("Parametrs/WorkCoffe");
                break;
            case 2:
                MatchThree.main.multvarka = true;
                text = LeanLocalization.GetTranslationText("Parametrs/WorkMulti");
                break;
            case 3:
                MatchThree.main.icebox = true;
                text = LeanLocalization.GetTranslationText("Parametrs/WorkIcebox");
                break;
            case 4:
                MatchThree.main.shower = true;
                text = LeanLocalization.GetTranslationText("Parametrs/WorkShow");
                break;
            case 5:
                MatchThree.main.bed = true;
                text = LeanLocalization.GetTranslationText("Parametrs/Workbed");
                break;
            case 6:
                MatchThree.main.computer = true;
                flowchart.SetBooleanVariable("HaveCompDialog", false);
                text = LeanLocalization.GetTranslationText("Parametrs/Workcomp");
                break;
            case 7:
                IncriseMoneyInOne = System.Math.Round(IncriseMoneyInOne, 2);
                text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/WorkcompUp"), (int)(IncriseMoneyInOne * 100));
                IncriseMoney += IncriseMoneyInOne;
                break;
            default:
                Debug.LogError("Ошибка увеличения параметра");
                text = "Ошибка увеличения параметра";
                break;
        }

        return text;
    }

    public string NotLuckFixDevice()
    {
        string text;
        switch ((int)((LevelProfile.main.level - 1) / 3f) + 1)
        {
            case 1:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakCoffe");
                break;
            case 2:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakMulty");
                break;
            case 3:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakIcebox");
                break;
            case 4:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakShow");
                break;
            case 5:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakBed");
                break;
            case 6:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakComp");
                break;
            case 7:
                text = LeanLocalization.GetTranslationText("Parametrs/BreakCompUp");
                break;
            default:
                Debug.LogError("Ошибка увеличения параметра");
                text = "Ошибка увеличения параметра";
                break;
        }

        return text;
    }

    public string GetMovesInGame()
    {
        return "" + MovesinGame;
    }


        // Увеличение параметров
        // 1: кофе
        // 2: еда
        // 4: гигиена
        // 5: кровать

        /*
         1 -кофеварка
         2- мультиварка
         3- холодильник
         4- душ
         5- кровать
         6- компьютер
        */

        public bool GetStatusDevice(int Device)
    {
        bool status;

        switch (Device)
        {
            case 1://кофе
                status = MatchThree.main.coffeevarka;
                break;
            case 2://еда
                status = MatchThree.main.multvarka;
                break;
            case 3://холодос
                status = MatchThree.main.icebox;
                break;
            case 4:// душ
                status = MatchThree.main.shower;
                break;
            case 5:// кровать
                status = MatchThree.main.bed;
                break;
            case 6:
                status = MatchThree.main.computer;
                break;
            default:
                status = false;
                Debug.LogError("Ошибка статуса");
                break;
        }
        return status;
    }

    // Увеличение параметров
    // 1: кофе
    // 2: еда
    // 4: гигиена
    // 5: кровать
    public void GetArkashaStoryTextAndAddParameter(int Device)
    {
        
        int random = Random.Range(1, 4);
        int AddTime = Random.Range(15, 21);
        AddititionParameters = BaseAddParametr + Random.Range(0, 11);
        // string StoryText;
        flowchart.SetBooleanVariable("GoodNews", true);  // задаю положительную новость, если это не так, потом задаю отрицательную
        switch (Device)
        {
            case 1://кофе
                if (numcoffee > 0)
                {
                    AudioAssistant.Shot("Drink");
                    numcoffee--;
                    MatchThree.main.coffee += AddititionParameters;
                    MatchThree.main.energy += AddititionParameters/5;
                    SteamAchiments.main.TakeAchievement("DRINK");
                    Debug.Log("Увеличение кофе на " + AddititionParameters);
                    flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("ParametrsUse/VoicCoffe"), AddititionParameters, AddititionParameters / 5, numcoffee));
                    if (random == 1)
                    {
                        MatchThree.main.coffeevarka = false;
                        //добавить звук сломавшейся вещи
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadCoffe"));
                        flowchart.SetBooleanVariable("DeviceStatus", false);  
                        flowchart.SetBooleanVariable("GoodNews", false);
                    }
                    else
                    {
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadCoffe2"));
                    }
                    MatchThree.main.TimeMinute += AddTime;
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceCoffe2"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadCoffe3"));
                    Debug.Log("Кончались запасы");
                    flowchart.SetBooleanVariable("GoodNews", false);
                }
                StartNovel.main.MesEndOptions();
                break;
            case 2://еда
                if (numproducts > 0)
                {
                    AudioAssistant.Shot("Eat");
                    numproducts--;
                    MatchThree.main.eat += AddititionParameters;
                    SteamAchiments.main.TakeAchievement("EAT");
                    Debug.Log("Увеличение еды на " + AddititionParameters);
                    flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("ParametrsUse/VoceEat"), AddititionParameters, numproducts));
                    if (random == 1)
                    {
                        MatchThree.main.multvarka = false;
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadEat"));
                        flowchart.SetBooleanVariable("DeviceStatus", false);
                        flowchart.SetBooleanVariable("GoodNews", false);
                    }
                    else
                    {
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadEat2"));
                    }
                    MatchThree.main.TimeMinute += AddTime;
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceEat2"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadEat3"));
                    Debug.Log("Кончались запасы");
                    flowchart.SetBooleanVariable("GoodNews", false);
                }
                StartNovel.main.MesEndOptions();
                break;
            case 3://холодос
                if (MatchThree.main.money >= (int)(PriceProduct * Priceincrease * NDS * NumProductPay))
                {

                    StartCoroutine(MoneyMinusAnim());
                    MatchThree.main.money -= (int)(PriceProduct * Priceincrease * NDS * NumProductPay);
                    MoneyOutgo += (int)(PriceProduct * Priceincrease * NDS * NumProductPay);
                    numproducts += NumProductPay;
                    Debug.Log("Увеличение запасов продуктов на " + NumProductPay);
                    flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("ParametrsUse/VoceIceBox"), NumProductPay));
                    if (random == 1)
                    {
                        MatchThree.main.icebox = false;
                        //добавить звук сломавшейся вещи
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox"));
                        flowchart.SetBooleanVariable("DeviceStatus", false);
                        flowchart.SetBooleanVariable("GoodNews", false);
                    }
                    else
                    {
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox2"));
                    }
                    MatchThree.main.TimeMinute += AddTime;
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIceBox2"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox3"));
                    Debug.Log("Кончались деньги");
                    flowchart.SetBooleanVariable("GoodNews", false);
                }
                StartNovel.main.MesEndOptions();
                break;
            case 4:// душ
                AudioAssistant.Shot("Shower");
                MatchThree.main.hygiene += AddititionParameters;
                MatchThree.main.energy += AddititionParameters/6;
                Debug.Log("Увеличение гигиены на " + AddititionParameters);
                flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("ParametrsUse/VoceHyg"), AddititionParameters, AddititionParameters / 6 + "."));
                if (random == 1)
                {
                    MatchThree.main.shower = false;
                    //добавить звук сломавшейся вещи
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadHyg"));
                    flowchart.SetBooleanVariable("DeviceStatus", false);
                    flowchart.SetBooleanVariable("GoodNews", false);
                }
                else
                {
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadHyg2"));
                }
                MatchThree.main.TimeMinute += AddTime;
                StartNovel.main.MesEndOptions();
                break;
            case 5:// кровать
                if (MatchThree.main.TimeHour >= 21 || MatchThree.main.TimeHour <= 2)  // true если хоть один true
                {
                    SteamAchiments.main.TakeAchievement("BED");
                    flowchart.SetBooleanVariable("NotCompDialog", false);  // для того, чтобы можно было вызывать диплог со сломаным пк
                    AudioAssistant.Shot("Bed");
                    // flowchart.SetStringVariable("VoicText", "Увеличение энергии."); - В функции NewDay это есть
                    if (random != 1)
                    {
                        MatchThree.main.bed = false;
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadBed"));
                        flowchart.SetBooleanVariable("DeviceStatus", false);
                        flowchart.SetBooleanVariable("GoodNews", false);
                    }
                    else
                    {
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadBed2"));
                    }

                    UIAssistant.main.ShowPage("InBed");
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceBed"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadBed3"));
                    flowchart.SetBooleanVariable("GoodNews", false);
                    StartNovel.main.MesEndOptions();
                }

                break;
            case 6://комп
                AudioAssistant.Shot("Keyboard");
                UIAssistant.main.ShowPage("ComputerUI");
                flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceComp"));
                flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadComp"));
                flowchart.SetBooleanVariable("NotCompDialog", false);  // для того, чтобы можно было вызывать диплог со сломаным пк
                break;
            default:
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                Debug.LogError("Ошибка статуса");
                break;
        }
    }

    public void SecondGetArkashaStoryTextAndAddParameter(int Device)
    {
        int AddTime = Random.Range(15, 21);
        int random = Random.Range(1, 4);
        AddititionParameters = BaseAddParametr + Random.Range(0, 11);
        flowchart.SetBooleanVariable("GoodNews", true);
        switch (Device)
        {
            case 1://IceBoxOption3
                UIAssistant.main.ShowPage("IceBoxUI");
                AudioAssistant.Shot("Fridge");
                if (numproducts < 3 && numcoffee < 3)
                {
                    flowchart.SetBooleanVariable("GoodNews", false);
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIcebox3"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox4"));
                }
                else if (numproducts < 3)
                {
                    flowchart.SetBooleanVariable("GoodNews", false);
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIcebox4"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox5"));
                }
                else if (numcoffee < 3)
                {
                    flowchart.SetBooleanVariable("GoodNews", false);
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIcebox5"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox6"));
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIcebox6"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox7"));
                }
                break;
            case 2://еда
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                break;
            case 3://холодос
                if (MatchThree.main.money >= (int)(PriceCoffee * Priceincrease * NDS * NumProductPay))
                {
                    StartCoroutine(MoneyMinusAnim());
                    MatchThree.main.money -= (int)(PriceCoffee * Priceincrease * NDS * NumProductPay);
                    MoneyOutgo += (int)(PriceCoffee * Priceincrease * NDS * NumProductPay);
                    numcoffee += NumProductPay;
                    Debug.Log("Увеличение запасов кофе на " + NumProductPay);

                    flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("ParametrsUse/VoceIcebox7"), NumProductPay));
                    if (random == 1)
                    {
                        MatchThree.main.icebox = false;
                        //добавить звук сломавшейся вещи
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox8"));
                        flowchart.SetBooleanVariable("DeviceStatus", false);
                        flowchart.SetBooleanVariable("GoodNews", false);
                    }
                    else
                    {
                        flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox9"));
                    }
                    MatchThree.main.TimeMinute += AddTime;
                }
                else
                {
                    flowchart.SetStringVariable("VoicText", LeanLocalization.GetTranslationText("ParametrsUse/VoceIceBox2"));
                    flowchart.SetStringVariable("ArkashaText", LeanLocalization.GetTranslationText("ParametrsUse/ArkadIcebox3"));
                    flowchart.SetBooleanVariable("GoodNews", false);
                    Debug.Log("Кончались деньги");
                }
                StartNovel.main.MesEndOptions();
                break;
            case 4:// душ
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                break;
            case 5:// кровать
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                break;
            case 6://комп
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                break;
            default:
                flowchart.SetStringVariable("ArkashaText", "Ошибка статуса");
                Debug.LogError("Ошибка статуса");
                break;
        }
    }

    public void StartFixDevice (int Device)
    {
        switch (Device)
        {
            case 1://кофе
                MatchThree.main.StartForLevel(Random.Range(1, 4));
                break;
            case 2://еда
                MatchThree.main.StartForLevel(Random.Range(4, 7));
                break;
            case 3://холодос
                MatchThree.main.StartForLevel(Random.Range(7, 10));
                break;
            case 4:// душ
                MatchThree.main.StartForLevel(Random.Range(10, 13));
                break;
            case 5:// кровать
                MatchThree.main.StartForLevel(Random.Range(13, 16));
                break;
            case 6://комп
                MatchThree.main.StartForLevel(Random.Range(16, 19));
                break;
            case 7://компUP
                MatchThree.main.StartForLevel(Random.Range(19, 22));
                break;
            default:
                Debug.LogError("Ошибка загрузки уровня");
                break;
        }
    }

    public void TextStatusDevice ()
    {
        if (MatchThree.main.coffeevarka)
        {
            TexStatusCoffee.color = Color.green;
            TexStatusCoffee.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
         else
        {
            TexStatusCoffee.color = Color.red;
            TexStatusCoffee.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }

        if (MatchThree.main.multvarka)
        {
            TexStatusMulti.color = Color.green;
            TexStatusMulti.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
        else
        {
            TexStatusMulti.color = Color.red;
            TexStatusMulti.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }

        if (MatchThree.main.icebox)
        {
            TexStatusIcebox.color = Color.green;
            TexStatusIcebox.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
        else
        {
            TexStatusIcebox.color = Color.red;
            TexStatusIcebox.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }

        if (MatchThree.main.shower)
        {
            TexStatusShower.color = Color.green;
            TexStatusShower.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
        else
        {
            TexStatusShower.color = Color.red;
            TexStatusShower.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }

        if (MatchThree.main.bed)
        {
            TexStatusBed.color = Color.green;
            TexStatusBed.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
        else
        {
            TexStatusBed.color = Color.red;
            TexStatusBed.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }

        if (MatchThree.main.computer)
        {
            TexStatusComputer.color = Color.green;
            TexStatusComputer.text = LeanLocalization.GetTranslationText("Parametrs/Work");
        }
        else
        {
            TexStatusComputer.color = Color.red;
            TexStatusComputer.text = LeanLocalization.GetTranslationText("Parametrs/Break");
        }
    }

    public bool NewDay (int TimeSleep)
    {
        
        if (MatchThree.main.LifeDays==5)
            SteamAchiments.main.TakeAchievement("5_DAYS");
        if (MatchThree.main.LifeDays==10)
            SteamAchiments.main.TakeAchievement("10_DAYS");

        flowchart.SetStringVariable("VoicText", System.String.Format(LeanLocalization.GetTranslationText("Parametrs/NewDayVoic"), TimeSleep * AddEnergy));
        if (LastNalogDay!=MatchThree.main.LifeDays)
        {
            LastNalogDay = MatchThree.main.LifeDays;
            TexPayRent.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/NewDayPayRent"), (int)(Rent * Rentincrease));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PayRent ()
    {
        if (MatchThree.main.money >= (int)(Rent * Rentincrease))
        {
            StartCoroutine(MoneyMinusAnim());
            SteamAchiments.main.TakeAchievement("PAY");
            ChangeNalog();
            MatchThree.main.money -= (int)(Rent * Rentincrease);
            MoneyOutgo += (int)(Rent * Rentincrease);
            UIAssistant.main.ShowPage("NewNalogPopup");
        }

       else
        {
            AudioAssistant.Shot("YouLose");
            UIAssistant.main.ShowPage("YouLoseGame");
        }
    }

    public bool ChekLoseGame ()
    {
        if (MatchThree.main.eat <= 0 || MatchThree.main.energy <= 0 || MatchThree.main.hygiene <= 0 || MatchThree.main.coffee <= 0)
        {
            AudioAssistant.Shot("YouLose");
            UIAssistant.main.ShowPage("YouLoseGame");
            return true;
        }
        else
            return false;
    }

    public void ChangeNalog ()
    {
        int random = Random.Range(1, 5);
        Debug.Log("random = " + random);
        double Incrise = System.Math.Round(Random.Range(IncriseNalogiInOneMin, IncriseNalogiInOneMax), 2);
        switch (random)
        {
            case 1:
                if (Nalogi > 0.4)
                {
                    Nalogi -= Incrise;
                    TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNNalogName");
                    TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNNalogDes");
                    TexIncrease.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNNalogIncr"), (int)(Incrise * 100));
                    TexNewNalog.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNNalogNewNalog"), (100 - (int)(Nalogi * 100)));
                }
                else
                {
                    TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNNotName");
                    TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNNotDes");
                    TexIncrease.text = "'_'";
                    TexNewNalog.text = "";
                }       
                break;
            case 2:
                NDS += Incrise;
                TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNNDSName");
                TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNNDSDes");
                TexIncrease.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNNDSIncr"), (int)(Incrise * 100));
                TexNewNalog.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNNDSNewNalog"), ((int)(NDS * 100) - 100));
                break;
            case 3:
                Priceincrease += Incrise;
                TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNPriceName");
                TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNPriceDes");
                TexIncrease.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNPriceIncr"), (int)(Incrise * 100));
                TexNewNalog.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNPriceNewNalog"), ((int)(Priceincrease * 100) - 100));
                break;
            case 4:
                Rentincrease += Incrise;
                TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNRentName");
                TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNRentDes");
                TexIncrease.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNRentIncr"), (int)(Incrise * 100));
                TexNewNalog.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/CNRentNewNalog"), ((int)(Rentincrease * 100) - 100));
                break;

            default:
                TexNameNalog.text = LeanLocalization.GetTranslationText("Parametrs/CNNotName");
                TexDescriptions.text = LeanLocalization.GetTranslationText("Parametrs/CNNotDes");
                TexIncrease.text = "'_'";
                TexNewNalog.text = "";
                break;
        }

    }

    IEnumerator MoneyMinusAnim()
    {
        AudioAssistant.Shot("Coin");
        MoneyAnim.SetBool("Minus", true);
        yield return new WaitForSeconds(0.2f);
        MoneyAnim.SetBool("Minus", false);
    }

    IEnumerator MoneyAddAnim()
    {
       SteamAchiments.main.TakeAchievement("FIRST_MONEY");
        AudioAssistant.Shot("AddMoney");
        MoneyAnim.SetBool("Add", true);
        yield return new WaitForSeconds(0.1f);
        MoneyAnim.SetBool("Add", false);
    }


    private void Update()
    {
        if (MatchThree.main.TimeMinute>=60)
        {
            MatchThree.main.TimeMinute -= 60;
            MatchThree.main.TimeHour++;
            if (MatchThree.main.computer)
            {
                StartCoroutine(MoneyAddAnim());
                int random = Random.Range(1, 11);
                MatchThree.main.money += (int)(AddMoney * Nalogi * IncriseMoney);
                MoneyProfit += (int)(AddMoney * Nalogi * IncriseMoney);
                if (random == 1)
                {
                    MatchThree.main.computer = false;
                    ChangeBG.main.UpdateStatusDevice();
                    AudioAssistant.Shot("Break");
                    StartNovel.main.MesCompMinus();
                }
            }
        }

        if (MatchThree.main.TimeHour>=24)
        {
            MatchThree.main.TimeHour -= 24;
            MatchThree.main.LifeDays++;
        }

        if (MatchThree.main.eat>=100)
        {
            MatchThree.main.eat = 100;
        }
        if (MatchThree.main.energy >= 100)
        {
            MatchThree.main.energy = 100;
        }
        if (MatchThree.main.hygiene >= 100)
        {
            MatchThree.main.hygiene = 100;
        }
        if (MatchThree.main.coffee >= 100)
        {
            MatchThree.main.coffee = 100;
        }


        SlEat.value = MatchThree.main.eat;
        SlEnergy.value = MatchThree.main.energy;
        SlCoffee.value = MatchThree.main.coffee;
        SlHygiene.value = MatchThree.main.hygiene;
        

        TeEat.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeEat"), MatchThree.main.eat);
        TeEnergy.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeEnergy"), MatchThree.main.energy);
        TexCoffee.text= System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeCoffe"), MatchThree.main.coffee);
        TexHygiene.text= System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeHygiene"), MatchThree.main.hygiene);
        TexMoney.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeMoney"), MatchThree.main.money);

        TexDays.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeDay"), MatchThree.main.LifeDays);
        TexTime.text = System.String.Format(LeanLocalization.GetTranslationText("Parametrs/TeTime"), MatchThree.main.TimeHour, (MatchThree.main.TimeMinute >= 10 ? "" + MatchThree.main.TimeMinute : "0" + MatchThree.main.TimeMinute));
    }


}
