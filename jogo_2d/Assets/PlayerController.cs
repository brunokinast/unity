using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Game Manager
    public GameManager GameManager;
    //Movement
    public float speed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D player;
    private bool facingRight = true;
    [Range(1,10)]
    public int jumpQuantity;
    [Range(1f, 3f)]
    public float jumpGravity;
    private int extraJumps;
    //Ground check
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

	// Use this for initialization
	void Start () {
        extraJumps = jumpQuantity;
        player = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(moveInput * speed, player.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        Debug.Log(extraJumps);
        Debug.Log(isGrounded);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded == true)
        {
            extraJumps = jumpQuantity;
            player.gravityScale = 1;
        }
        if (isGrounded != true && player.velocity.y < -0.01f && player.velocity.y > -0.5f)
        {
            player.gravityScale = jumpGravity;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (extraJumps > 0)
            {
                player.gravityScale = 1;
                StartCoroutine(Jump());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameManager.addCoin();
        }
    }

    //Flips Sprite to face move direction
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    //Jump and reset Jump count, prevents player from spamming key(?)
    IEnumerator Jump()
    {
        player.velocity = Vector2.up * jumpForce;
        extraJumps--;
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.3f);
        extraJumps = jumpQuantity;
    }
}
