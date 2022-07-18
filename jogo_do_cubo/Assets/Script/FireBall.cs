using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    public float speed = 0.00003f;
    private Rigidbody thisrigid;
	// Use this for initialization
	void Start () {
        thisrigid = GetComponent<Rigidbody>();
        thisrigid.AddForce(speed * transform.forward);
        StartCoroutine(deathTimer());
	}

    void OnCollisionEnter(Collision collision)
    {
        print("collided");
        if (collision.gameObject.tag == "enemyPurple")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (!(collision.gameObject.tag == "Player"))
        {
            print("Collision with " + collision.transform.tag + " and " + gameObject.tag);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyPurple")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
	}

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
