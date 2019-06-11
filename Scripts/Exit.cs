using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    public void Exit_from_game()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
