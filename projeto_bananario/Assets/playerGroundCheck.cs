using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundCheck : MonoBehaviour {
    public static bool isGrounded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Foreground")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foreground")
        {
            isGrounded = false;
        }
    }
}
