using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour {
    public bool playerIsOn;
	// Use this for initialization
	void Start () {
        playerIsOn = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOn = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
