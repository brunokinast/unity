using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {
    Animation spikeanimation;
	// Use this for initialization
	void Start () {
        spikeanimation = GetComponent<Animation>();
        StartCoroutine(Go());
	}
	
    IEnumerator Go()
    {
        while (true)
        {
            spikeanimation.Play();
            yield return new WaitForSeconds(2f);
        }
    }
}
