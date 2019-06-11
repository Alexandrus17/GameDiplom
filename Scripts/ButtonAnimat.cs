using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimat : MonoBehaviour {

   void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.6f, 0.4f, 0.6f);
    }
    void OnMouseExit()
    {
        transform.localScale = new Vector3(0.55f, 0.33f, 0.6f);
    }


}
