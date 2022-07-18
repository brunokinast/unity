using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    private playerGroundCheck playerGroundCheck;
    public float moveSpeed = 5f;
    public Animator animator;
    private bool facingRight = true;
    private Rigidbody2D rbPlayer;
    private void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody2D>();
        playerGroundCheck = GetComponentInChildren<playerGroundCheck>();
    }
    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }else if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        transform.position += movement * Time.deltaTime * moveSpeed;
        if (Input.GetAxisRaw("Jump") > 0 && playerGroundCheck.isGrounded)
        {
            rbPlayer.velocity = Vector3.zero;
            rbPlayer.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}
