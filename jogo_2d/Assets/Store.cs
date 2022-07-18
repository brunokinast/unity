using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {

    public GameObject pressEPrefab;
    private GameObject pressE;
    private SpriteRenderer pressESprite;

    private void Start()
    {
        pressE = Instantiate(pressEPrefab, new Vector2(transform.position.x, transform.position.y + 0.9f) , Quaternion.identity);
        pressESprite = pressE.GetComponent<SpriteRenderer>();
        pressESprite.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressESprite.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressESprite.enabled = false;
        }
    }
}
