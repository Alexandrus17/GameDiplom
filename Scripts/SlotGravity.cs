using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Berry.Utils;

[RequireComponent (typeof (Slot))]
public class SlotGravity : MonoBehaviour {

	public Slot slot;

    public Side gravityDirection = Side.Bottom;
    public Side fallingDirection = Side.Null;

		public bool shadow;
	
	void  Awake (){
		slot = GetComponent<Slot>();
	}

	public static void  Reshading () { 
        foreach (SlotGravity sg in GameObject.FindObjectsOfType<SlotGravity>())
            sg.shadow = true;

        Slot slot;
        List<Slot> stock = new List<Slot>();
        List<SlotGenerator> generator = new List<SlotGenerator>(GameObject.FindObjectsOfType<SlotGenerator>());
        foreach (SlotGenerator sgen in generator) {
            slot = sgen.slot;
            stock.Clear();
            while (slot && !slot.block && slot.slotGravity.shadow && !stock.Contains(slot)) {
                slot.slotGravity.shadow = false;
                stock.Add(slot);
                slot = slot[slot.slotGravity.gravityDirection];
            }
            sgen.slot.slotGravity.shadow = false;
        }

	}
	
	void  Update (){
        if (slot && slot.chip && !slot.chip.busy)
            GravityReaction();
	}

	public void  GravityReaction (){
        if (!MatchThree.main.isPlaying) return; 
		
        if (!slot || !slot.chip) return;

        if (Chip.gravityBlockers.Count > 0) return;

		if (transform.position != slot.chip.transform.position) return; 
		
        if (!slot[gravityDirection] || slot[gravityDirection].block)
            return; 

        if (slot[gravityDirection] && slot[gravityDirection].chip && slot[gravityDirection].chip.busy)
            return;
        
        if (!slot[gravityDirection].chip) {
            slot[gravityDirection].chip = slot.chip;
			GravityReaction();
			return;
		} 
		if (Random.value > 0.5f) { 
			SlideLeft();
			SlideRight();
		} else {
			SlideRight();
			SlideLeft();	
		}
	}



    void SlideLeft() {
        Side cw45side = Utils.RotateSide(gravityDirection, 1);
        Side cw90side = Utils.RotateSide(gravityDirection, 2);

        if (slot[cw45side] 
            && !slot[cw45side].block 
            && ((slot[gravityDirection] && slot[gravityDirection][cw90side]) || (slot[cw90side] && slot[cw90side][gravityDirection])) 
            && !slot[cw45side].chip 
            && slot[cw45side].GetShadow() 
            && !slot[cw45side].GetChipShadow()) { 
            slot[cw45side].chip = slot.chip; 
		}
	}

    void SlideRight() {
        Side ccw45side = Utils.RotateSide(gravityDirection, -1);
        Side ccw90side = Utils.RotateSide(gravityDirection, -2);

        if (slot[ccw45side] 
            && !slot[ccw45side].block 
            && ((slot[gravityDirection] && slot[gravityDirection][ccw90side]) || (slot[ccw90side] && slot[ccw90side][gravityDirection])) 
            && !slot[ccw45side].chip 
            && slot[ccw45side].GetShadow() 
            && !slot[ccw45side].GetChipShadow()) {
			slot[ccw45side].chip = slot.chip; 
		}
	}
}