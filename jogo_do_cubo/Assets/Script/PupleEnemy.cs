using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupleEnemy : MonoBehaviour {
    public GameObject ground;
    public float speed;
    private GroundTrigger script;
    private GameObject Player;
    private Vector3 spawnPos;
	// Use this for initialization
	void Start () {
        script = ground.GetComponent<GroundTrigger>();
        spawnPos = this.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void FixedUpdate()
    {
        if (script.playerIsOn)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(transform.position, spawnPos, speed * Time.deltaTime);
        }
    }
}
