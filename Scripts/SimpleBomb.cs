using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Berry.Utils;


[RequireComponent (typeof (Chip))]
public class SimpleBomb : IBomb, IAnimateChip, IChipLogic {

    

	Chip _chip;
	int birth; 

    public Chip chip {
        get {
            return _chip;
        }
    }

	void  Awake (){
		_chip = GetComponent<Chip>();
        chip.chipType = "SimpleBomb";
		birth = MatchThree.main.eventCount;
		AudioAssistant.Shot ("CreateBomb");
	}

    
    public IEnumerator Destroying() {
        if (birth == MatchThree.main.eventCount) {
			chip.destroying = false;
			yield break;
		}
		
		chip.busy = true;

        yield return new WaitForSeconds(0.1f);

        chip.Play("Destroying");
        AudioAssistant.Shot("BombCrush");

		
		yield return new WaitForSeconds(0.05f);

        AnimationAssistant.main.Explode(transform.position, 5, 10);

        yield return 0;
        int destroy = 0;
        foreach (Side side in Utils.allSides)
        {
            NeighborMustDie(chip.slot.coord + Utils.SideOffset(side));
            destroy++;
        }
        Debug.Log("AnimationAssistant.main.Animations(destroy);");
        yield return new WaitForSeconds(0.1f);

        chip.ParentRemove();
		chip.busy = false;
		
		while (chip.IsPlaying("Destroying"))
            yield return 0;

		Destroy(gameObject);
	}
	
	void  NeighborMustDie (int2 coord){
        
        Slot s = Slot.GetSlot(coord);
		if (s) {
			if (s.chip) {
                s.chip.SetScore(0.3f);
                s.chip.DestroyChip();
                

            }
			FieldAssistant.main.BlockCrush(coord, false);
		}

	}

    public List<Chip> GetDangeredChips(List<Chip> stack) {
        if (stack.Contains(chip))
            return stack;

        stack.Add(chip);

        Slot slot;

        foreach (Side s in Utils.allSides) {
            slot = chip.slot[s];
            if (slot && slot.chip) {
                stack = slot.chip.GetDangeredChips(stack);
            }
        }

        return stack;
    }

    #region Mixes
    public void SimpleMix(Chip secondary) {
        StartCoroutine(SimpleMixRoutine(secondary));
    }

    IEnumerator SimpleMixRoutine(Chip secondary) {
        chip.busy = true;
        chip.destroyable = false;
        MatchThree.main.EventCounter();

        Transform effect = ContentAssistant.main.GetItem("SimpleMixEffect").transform;
        effect.SetParent(Slot.folder);
        effect.position = transform.position;
        effect.GetComponent<Animation>().Play();
        AudioAssistant.Shot("BombCrush");
        MatchThree.main.EventCounter();

        chip.Minimize();

        MatchThree.main.EventCounter();
        int2 key = new int2();
        for (key.x = 0; key.x < LevelProfile.main.width; key.x++)
            for (key.y = 0; key.y < LevelProfile.main.height; key.y++)
                if (Mathf.Abs(chip.slot.x - key.x) + Mathf.Abs(chip.slot.y - key.y) <= 3)
                    Crush(key);

        AnimationAssistant.main.Explode(transform.position, 5, 30);

        yield return new WaitForSeconds(0.6f);

        chip.busy = false;

        while (chip.IsPlaying())
            yield return 0;

        FieldAssistant.main.BlockCrush(chip.slot.coord, false);

        chip.HideChip(false);
    }

    public void Crush(int2 coord) {
        Slot s = Slot.GetSlot(coord);
        FieldAssistant.main.BlockCrush(coord, false, true);
        if (s && s.chip) {
            Chip c = s.chip;
            c.SetScore(0.3f);
            c.DestroyChip();
        }
    }

    public string[] GetClipNames() {
        return new string[] { "Destroying" };
    }

    public string GetChipType() {
        return "SimpleBomb";
    }

    public bool IsMatchable() {
        return true;
    }

    public int GetPotencial() {
        return 7;
    }
    #endregion
}