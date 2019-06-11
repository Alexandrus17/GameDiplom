using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Berry.Utils;

public class Level : MonoBehaviour {

    public static Dictionary<int, LevelProfile> all = new Dictionary<int, LevelProfile>();
    public LevelProfile profile;

    void Awake() {
      
        profile.level = transform.GetSiblingIndex() + 1;

        if (!all.ContainsKey(profile.level))
            all.Add(profile.level, profile);

        if (!Application.isEditor)
        {
            Destroy(gameObject);
        }
    }

    public static void LoadLevel(int key) { 
        if (CPanel.uiAnimation > 0)
            return;

        if (!all.ContainsKey(key))
            return;

        LevelProfile.main = all[key];

        UIAssistant.main.ShowPage("LevelSelectedPopup");
    }
    

#if UNITY_EDITOR
    public static void TestLevel(int l) {
        LevelProfile.main = all[l];
        FieldAssistant.main.StartLevel();
        PlayerPrefs.DeleteKey("TestLevel");
    }
#endif
    
    
    public static void LoadMyLev (int key)
    {
        if (CPanel.uiAnimation > 0)
            return;

        if (!all.ContainsKey(key))
            return;

        Debug.Log("Запускается " + key + " Уровень");

        ShowNeededpage.main.Room = 7;  

        LevelProfile.main = all[key];  



      if (key <= 18)
        {
            Debug.Log("Усложнение уровня на" +MatchThree.main.LifeDays);
            LevelProfile.main.firstStarScore = 100 + (5 * (MatchThree.main.LifeDays - 1));
            LevelProfile.main.secondStarScore = 200 + (10 * (MatchThree.main.LifeDays - 1));
            LevelProfile.main.thirdStarScore = 300 + (15 * (MatchThree.main.LifeDays - 1));
        }
      else
        {
            Debug.Log("Усложнение Увеличения дохода на" +MatchThree.main.LifeDays);
            LevelProfile.main.firstStarScore = 200 + (10 * (MatchThree.main.LifeDays - 1));
            LevelProfile.main.secondStarScore = 400 + (20 * (MatchThree.main.LifeDays - 1));
            LevelProfile.main.thirdStarScore = 600 + (30 * (MatchThree.main.LifeDays - 1));
        }


        FieldAssistant.main.StartLevel();
        
    }
}

[System.Serializable]
public class LevelProfile {

    public static LevelProfile main; 
    public const int maxSize = 15; 
    
    public int levelID = 0; 
    public int level = 0; 
    
    public int width = 9;
    public int height = 9;
    public int colorCount = 6; 
    public int targetSugarDropsCount = 0; 
    public int firstStarScore = 100; 
    public int secondStarScore = 200; 
    public int thirdStarScore = 300; 

    
    
    
    

    
    
    public int limit = 30; 

    public List<SlotSettings> slots = new List<SlotSettings>();
    
    public LevelProfile GetClone() {
        LevelProfile clone = new LevelProfile();
        clone = (LevelProfile) MemberwiseClone(); 
        clone.levelID = -1;
        return clone;
    }
}

[System.Serializable]
public class SlotSettings { 
    public int2 position = new int2();
    public bool generator = false;
    public string chip = "";
    public int color_id = 0;
    public string block_type = "";
    public int block_level = 0;

    public SlotSettings(int _x, int _y) {
        position = new int2(_x, _y);
    }

    public SlotSettings(int2 _position) {
        position = _position;
    }

    public SlotSettings GetClone() {
        return MemberwiseClone() as SlotSettings;
    }
}