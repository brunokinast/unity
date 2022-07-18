using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCheck : MonoBehaviour {
    public bool wallAhead;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Foreground")
        {
            wallAhead = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Foreground")
        {
            wallAhead = false;
        }
    }
}
