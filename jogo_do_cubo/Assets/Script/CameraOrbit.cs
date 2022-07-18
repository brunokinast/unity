using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    private Camera thiscamera;
    public bool moving;
    private GameObject targetObject;
    private Vector3 offset;
    private float targetAngle = 0;
    const float rotationAmount = 3f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 30f;
    float fov;

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        thiscamera = GetComponent<Camera>();
        fov = thiscamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
        }
        thiscamera.fieldOfView = fov;
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                targetAngle -= 90.0f;
                moving = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                targetAngle += 90.0f;
                moving = true;
            }
        }
        if (targetAngle != 0)
        {
            Rotate();
        }
        else
        {
            moving = false;
        }
    }

    protected void Rotate()
    {

        float step = rSpeed * Time.deltaTime;
        float orbitCircumfrance = 2F * rDistance * Mathf.PI;
        float distanceDegrees = (rSpeed / orbitCircumfrance) * 360;

        if (targetAngle > 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }
    }
}
