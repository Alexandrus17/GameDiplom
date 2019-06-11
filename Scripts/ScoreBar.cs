using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent (typeof (Slider))]
public class ScoreBar : MonoBehaviour {

    public static System.Action<StarType> onStarGet = delegate{};

	Slider slider;

	float target = 0;
	float current = 0;

	void Awake () {
		slider = GetComponent<Slider> ();
	}

	void OnEnable () {
		if (MatchThree.main == null) return;
		current = MatchThree.main.score;
	}

	void Update () {
		target = Mathf.Min(MatchThree.main.score, LevelProfile.main.thirdStarScore);
		current = Mathf.MoveTowards (current, target, Time.unscaledDeltaTime * LevelProfile.main.thirdStarScore * 0.3f);
		slider.value = current / LevelProfile.main.thirdStarScore;
        if (MatchThree.main.stars < 1 && current >= LevelProfile.main.firstStarScore) {
            MatchThree.main.stars = 1;
              ChangeBG.main.Chenge(LevelProfile.main.level, 1);
            onStarGet.Invoke(StarType.First);
        }
        if (MatchThree.main.stars < 2 && current >= LevelProfile.main.secondStarScore) {
            MatchThree.main.stars = 2;
              ChangeBG.main.Chenge(LevelProfile.main.level, 2);
            StartNavel.main.FinishButton.GetComponentInChildren<Text>().text = Lean.Localization.LeanLocalization.GetTranslationText("Match/ScoreSt2");
            onStarGet.Invoke(StarType.Second);
        }
        if (MatchThree.main.stars < 3 && current >= LevelProfile.main.thirdStarScore) {
            MatchThree.main.stars = 3;
            ChangeBG.main.Chenge(LevelProfile.main.level, 2);
            onStarGet.Invoke(StarType.Third);
        }
	}
}
