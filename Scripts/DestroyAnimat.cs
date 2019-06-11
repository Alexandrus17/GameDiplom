using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimat : MonoBehaviour {
    public float moveSpeed = 1f;
    public float SecDestroy = 5f;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, SecDestroy);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * Time.deltaTime * moveSpeed;
    }
}
