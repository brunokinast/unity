using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
    [Tooltip("1:Destroy 2:Move")]
    public bool trigger = true;
    public int Case;
    public float Value1;
    public Vector3 Vector;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        switch (Case)
        {
            case 1:
                if (trigger)
                {
                    Destroy(this.gameObject, Value1);
                    Case = 0;
                }
            break;
            case 2:
                if (trigger)
                {
                    if (transform.position != Vector)
                    {
                        transform.position = Vector3.MoveTowards(this.transform.position, Vector, Value1 * Time.deltaTime);
                    }
                    else
                    {
                        Case = 0;
                    }
                }
            break;
            default:
                break;
        }
	}
}
