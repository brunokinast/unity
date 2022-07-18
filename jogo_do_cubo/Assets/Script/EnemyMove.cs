using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public Transform[] patrolPoint;
    public float enemySpeed;
    private int currentPoint;
	// Use this for initialization
	void Start () {
        transform.position = patrolPoint[0].position;
        currentPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position == patrolPoint[currentPoint].position)
        {
            currentPoint++;
        }
        if (currentPoint >= patrolPoint.Length)
        {
            currentPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoint[currentPoint].position, enemySpeed * Time.deltaTime);
	}
}
