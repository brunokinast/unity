using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour {
    public bool groundAhead;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Foreground")
        {
            groundAhead = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Foreground")
        {
            groundAhead = false;
        }
    }
}
