using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour {
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    private bool isItem = false;
	// Use this for initialization
	void Start () {
        posOffset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isItem)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
        }
 	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isItem = true;
        }
    }
}
