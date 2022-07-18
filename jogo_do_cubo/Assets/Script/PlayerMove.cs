using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    //Particles
    public GameObject deathParticle;
    //Camera
    public static GameObject cameraTransform;
    private CameraOrbit cameraScript;
    //GameHandler
    private GameHandler gamehandler;
    //Movement
    public float moveSpeed;
    public float maxSpeed;
    private Vector3 input;
    private Vector3 inputRaw;
    private Vector3 spawnPoint;
	private Rigidbody body;
    private Vector3 brake;
    //Items
    private bool hasUmbrella = false;
    private bool hasGun = false;
    private GameObject playerGun;
    private float shootTimer = 1f;
    public float shootDelay = 1f;
    public GameObject fireBall;


    // Use this for initialization
    void Start () {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera");
        gamehandler = new GameHandler();
        spawnPoint = transform.position;
        cameraScript = cameraTransform.GetComponent<CameraOrbit>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasGun)
        {
            playerGun = GameObject.FindGameObjectWithTag("gunItem");
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!cameraScript.moving)
                {
                    var lookPos = cameraTransform.transform.position - playerGun.transform.position;
                    lookPos.y = 0;
                    lookPos.x = -lookPos.x;
                    lookPos.z = -lookPos.z;
                    var rotation = Quaternion.LookRotation(lookPos);
                    playerGun.transform.rotation = Quaternion.Slerp(playerGun.transform.rotation, rotation, 1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!cameraScript.moving)
                {
                    var lookPos = cameraTransform.transform.position - playerGun.transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    playerGun.transform.rotation = Quaternion.Slerp(playerGun.transform.rotation, rotation, 1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!cameraScript.moving)
                {
                    var lookPos = cameraTransform.transform.position - playerGun.transform.position;
                    float tempPos;
                    lookPos.y = 0;
                    tempPos = lookPos.z;
                    lookPos.z = -lookPos.x;
                    lookPos.x = tempPos;
                    var rotation = Quaternion.LookRotation(lookPos);
                    playerGun.transform.rotation = Quaternion.Slerp(playerGun.transform.rotation, rotation, 1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!cameraScript.moving)
                {
                    var lookPos = cameraTransform.transform.position - playerGun.transform.position;
                    float tempPos;
                    lookPos.y = 0;
                    tempPos = lookPos.z;
                    lookPos.z = lookPos.x;
                    lookPos.x = -tempPos;
                    var rotation = Quaternion.LookRotation(lookPos);
                    playerGun.transform.rotation = Quaternion.Slerp(playerGun.transform.rotation, rotation, 1f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            this.StartCoroutine(Die());
        }
        if (other.tag == "Finish")
        {
            gamehandler.finishLevel();
        }
        if (other.tag == "Umbrella")
        {
            var otherCollider = other.GetComponent<BoxCollider>();
            otherCollider.enabled = false;
            hasUmbrella = true;
            other.transform.SetParent(this.transform);
            other.transform.rotation = Quaternion.identity;
            other.transform.localPosition = new Vector3(0.525f,0.6f,0f);
        }
        if (other.tag == "gunItem")
        {
            var gunEquip = other.gameObject;
            var otherCollider = other.GetComponent<BoxCollider>();
            otherCollider.enabled = false;
            hasGun = true;
            other.transform.SetParent(this.transform);
            other.transform.rotation = Quaternion.identity;
            other.transform.localPosition = Vector3.zero;
        }
        if (other.tag == "lavaCloud")
        {
            if (!hasUmbrella)
            {
                StartCoroutine(Die());
            }
        }
        if (other.tag == "enemyPurple")
        {
            StartCoroutine(Die());
        }
    }

    private void FixedUpdate()
    {
        #region Movement
        inputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = cameraTransform.transform.TransformDirection(inputRaw);
        input = Vector3.ProjectOnPlane(input, Vector3.up);
        body = GetComponent<Rigidbody>();
        if (Input.GetAxisRaw("Brake") != 1f)
        {
            if (body.velocity.magnitude < maxSpeed)
            {
                body.AddRelativeForce(input.normalized * moveSpeed);
            }
        }
        if (Input.GetAxisRaw("Brake") == 1f)
        {
            brake = new Vector3(-body.velocity.x, 0f, -body.velocity.z);
            body.AddForce(brake * 2);
        }

        if (body.position.y <= -6f)
        {
            StartCoroutine(Die());
        }

        #endregion
        if (Input.GetButtonDown("Respawn"))
        {
            StartCoroutine(Die());
        }
        if (hasGun)
        {
            if (Input.GetAxisRaw("Shoot") == 1f)
            {
                if (shootTimer >= shootDelay)
                {
                    shootTimer = 0;
                    Instantiate(fireBall, playerGun.transform.position, playerGun.transform.rotation);
                }
            }
            shootTimer += Time.deltaTime;
        }
    }

    IEnumerator Die()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Renderer render = GetComponent<Renderer>();
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            item.enabled = false;
        }
        collider.enabled = false;
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        render.enabled = false;
        body.isKinematic = true;
        transform.position = transform.position + new Vector3(0,0.1f,0);
        yield return new WaitForSeconds(2f);
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            item.enabled = true;
        }
        collider.enabled = true;
        render.enabled = true;
        body.isKinematic = false;
        body.velocity = Vector3.zero;
        this.transform.position = spawnPoint;
        StopCoroutine(Die());
    }
}
