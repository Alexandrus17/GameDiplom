using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerChips : MonoBehaviour { //     изменение бомбы в три в ряд в session и


    public Sprite[] BombImage;
   // public static ChangerChips main;
    float BOmbNum;  // номер арта с 0
    public SpriteRenderer ThisSpr; //текущий фон

    /*
     0- кофе
     1- мультиварка
     2- холодил
     3- душ
     4 - кровать
     5 - комп   
         */

    void Awake()
    {
    //    main = this;
        ThisSpr = GetComponent<SpriteRenderer>();
    }
    
    public void Start()
    {
        BOmbNum = (LevelProfile.main.level-1) /3f;
        ThisSpr.sprite = BombImage[(int)BOmbNum];
        Debug.Log("Chage "+ BombImage[(int)BOmbNum].name +" Lev - "+ LevelProfile.main.level);

    }
}
