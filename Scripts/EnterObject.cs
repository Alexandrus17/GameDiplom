using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterObject : MonoBehaviour
{
    public GameObject Around;

    // Start is called before the first frame update
    void OnMouseEnter()
    {
       Around.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        Around.SetActive(false);
    }
}
