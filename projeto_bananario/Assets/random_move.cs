using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_move : MonoBehaviour {
    public GameObject groundCheck;
    public GameObject wallCheck;
    public float moveSpeed = 2f;
    public Animator animator;
    private bool groundAhead;
    private bool wallAhead;
    private int walkTime;
    private bool isWalking;
    private int direction = -1;
    private int oldDirection = -1;
    private Vector3 movement;

    // Use this for initialization
    void Start () {
        walkRand();
	}
    private void Update()
    {
        groundAhead = groundCheck.GetComponent<groundCheck>().groundAhead;
        wallAhead = wallCheck.GetComponent<wallCheck>().wallAhead;
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (groundAhead && isWalking && wallAhead == false)
        {
            transform.position += movement * Time.deltaTime * moveSpeed;
            animator.SetBool("playWalk", true);
        }
        else
        {
            animator.SetBool("playWalk", false);
        }
    }
    private void walkRand()
    {
        if (groundAhead == false || wallAhead)
        {
            if (direction == 1)
            {
                transform.rotation = Quaternion.identity;
                direction = -1;
                oldDirection = 1;
                movement = new Vector3(direction, 0, 0);
            }
            else
            {
                transform.Rotate(new Vector3(0, 180, 0));
                direction = 1;
                oldDirection = -1;
                movement = new Vector3(direction, 0, 0);
            }
        }
        else
        {
            oldDirection = direction;
            do
            {
                direction = Random.Range(-1, 2);
            } while (direction == 0);
            flip();
            movement = new Vector3(direction, 0, 0);
        }
        walkTime = Random.Range(1, 3);
        StartCoroutine(walkTimer());
    }
    private IEnumerator walkTimer()
    {
        isWalking = true;
        while (walkTime != 0)
        {
            yield return new WaitForSeconds(1);
            walkTime--;
        }
        isWalking = false;
        yield return new WaitForSeconds(Random.Range(1,4));
        walkRand();
    }
    private void flip()
    {
        if (oldDirection == 1 && direction == -1)
        {
            transform.rotation = Quaternion.identity;
        }
        if (oldDirection == -1 && direction == 1)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
