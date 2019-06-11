using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG : MonoBehaviour { // изменение заднего фона в три в ряд в session и 

    public Sprite[] BGImage;
    public Sprite[] ArtImage;
    public Sprite[] ArtImageInRoom;

    public GameObject ___Space1___;

    public static ChangeBG main;
    int BGNum;  
    int ArtNum; // номер арта с 0
    public Image ThisBGSpr; //текущий фон
    public Image ThisArtSpr; //текущий изоражение
    public GameObject animNewArt;
    private Animator _animator;

    public GameObject ___Space___;

    public SpriteRenderer Sprcoffeevarka;
    public SpriteRenderer Sprmultvarka;
    public SpriteRenderer Spricebox;
    public SpriteRenderer Sprshower;
    public SpriteRenderer Sprbad;
    public SpriteRenderer Sprcomputer;

    // public float ChengeGirl;

    void Awake()
    {
        main = this;
        _animator = ThisArtSpr.GetComponent<Animator>();

    }

    public void ActiveArt (bool act)
    {
        ThisArtSpr.gameObject.SetActive(act);
    }

    
    public void Chenge(int level, int fon)
    {
            _animator.SetFloat("ChangeGirl", 1);
            StartCoroutine(CurAnimNewBG(level, fon));
          //  enabled = false;
 
    }
    
    public void UpdateStatusDevice()
    {
        if (MatchThree.main.coffeevarka)
            Sprcoffeevarka.sprite= ArtImageInRoom[1];
        else
            Sprcoffeevarka.sprite = ArtImageInRoom[0];

        if (MatchThree.main.multvarka)
            Sprmultvarka.sprite = ArtImageInRoom[3];
        else
            Sprmultvarka.sprite = ArtImageInRoom[2];

        if (MatchThree.main.icebox)
            Spricebox.sprite = ArtImageInRoom[5];
        else
            Spricebox.sprite = ArtImageInRoom[4];

        if (MatchThree.main.shower)
            Sprshower.sprite = ArtImageInRoom[7];
        else
            Sprshower.sprite = ArtImageInRoom[6];

        if (MatchThree.main.bed)
            Sprbad.sprite = ArtImageInRoom[9];
        else
            Sprbad.sprite = ArtImageInRoom[8];

        if (MatchThree.main.computer)
            Sprcomputer.sprite = ArtImageInRoom[11];
        else
            Sprcomputer.sprite = ArtImageInRoom[10];


        Parametrs.main.TextStatusDevice();
}

        public void ChengeArtAndBG(int level)
    {

        BGNum = (int)((level-1) / 3f)+1;
       
        Debug.Log("NumbFON - " + BGNum);
            ThisBGSpr.sprite = BGImage[BGNum];

        ArtNum = (int)((level - 1) / 3f) * 3;

        ThisArtSpr.sprite = ArtImage[ArtNum];
        Debug.Log("NumbArt - " + ArtNum);
    }

    IEnumerator CurAnimNewBG(int level, int fon)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 5, Screen.height/3, 20));
        Instantiate(animNewArt, pos, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.7f);
        ArtNum = (int)((level - 1) / 3f) * 3+fon;
        Debug.Log("NumbArt - " + ArtNum);
        ThisArtSpr.sprite = ArtImage[ArtNum];
        _animator.SetFloat("ChangeGirl", 0);
        yield return 0;
    }

}
