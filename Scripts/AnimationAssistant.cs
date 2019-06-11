using UnityEngine;
using System.Collections;

public class AnimationAssistant : MonoBehaviour {
	

    public GameObject star;
    public static AnimationAssistant main; 
	void  Awake (){
		main = this;
	}

	
	public void  Explode (Vector3 center, float radius, float force){
		Chip[] chips = GameObject.FindObjectsOfType<Chip>();
		Vector3 impuls;
		foreach(Chip chip in chips) {
			if ((chip.transform.position - center).magnitude > radius) continue;
			impuls = (chip.transform.position - center) * force;
			impuls *= Mathf.Pow((radius - (chip.transform.position - center).magnitude) / radius, 2);
			chip.impulse += impuls;
		}
	}

    
    public void Ferverk(int a) 
    {
        switch (a)
        {
            case 1:
                StartCoroutine(SpawnStar());
                break;
            case 0:
                StopAllCoroutines();
                break;
            default:
                Debug.Log("DEFAULT");
                break;
        }
    }

    IEnumerator SpawnStar()
    {
        while (true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(Screen.height / 2, Screen.height), 20));
            Instantiate(star, pos, Quaternion.Euler(0, 0, Random.Range(0f, 360)));
            yield return new WaitForSeconds(0.4f);
        }
    }



    public void TeleportChip(Chip chip, Slot target) {
		StartCoroutine (TeleportChipRoutine (chip, target));
	}

	IEnumerator TeleportChipRoutine (Chip chip, Slot target) {
		if (!chip.slot) yield break;
        if (chip.destroying) yield break;
        if (target.chip || target.block) yield break;

        Vector3 scale_target = Vector3.zero;
        target.chip = chip;
        chip.busy = true;
    
        scale_target.z = 1;
        while (chip.transform.localScale.x != scale_target.x) {
            chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 20);
            yield return 0;
            if (!chip) yield break;
        }

        chip.transform.localPosition = Vector3.zero;
        scale_target.x = 1;
        scale_target.y = 1;
        while (chip.transform.localScale.x != scale_target.x) {
            chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 20);
            yield return 0;
            if (!chip) yield break;
        }

        chip.busy = false;
    }

}