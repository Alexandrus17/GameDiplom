using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class ButtonSound : MonoBehaviour {

    

	
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	
	void OnClick () {
        AudioAssistant.Shot("UIButton");
	}
}
