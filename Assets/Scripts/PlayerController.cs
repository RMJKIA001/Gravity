﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private CharacterController cont;
    private float currCamPosition = 0;
    private bool isRunning = false;
    private float vert, hori;

    public Camera cam;
    public float mouseSensitiviy = 1;
    public float velocity = 0;
    public float jumpSpeed = 4;
    public float speed = 5;
    public bool ensared = false;
    public GameObject trappedText;
    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cont = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        float rotX = Input.GetAxis("Mouse X") * mouseSensitiviy;
        float rotY = Input.GetAxis("Mouse Y") * mouseSensitiviy;
        transform.Rotate(0, rotX, 0);

        currCamPosition -= rotY;
        float newRot = Mathf.Clamp(currCamPosition, -50, 50);
        cam.transform.localRotation = Quaternion.Euler(newRot, 0, 0);

        if(!ensared)
        {
            vert = Input.GetAxis("Vertical");
            hori = Input.GetAxis("Horizontal");
            velocity += Physics.gravity.y * Time.deltaTime;
        }
            
            bool isWalking = (vert != 0 || hori != 0);

            if (Input.GetKey(KeyCode.LeftShift) && isWalking)
            {
                vert *= 2f;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (Input.GetButtonDown("Jump") && cont.isGrounded)
            {
                velocity = jumpSpeed;
            }
        
        if(Input.GetKey(KeyCode.P))
        {
            trappedText.GetComponent<Text>().enabled = true;
            trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
            ensared = true;
        }

        
        Vector3 move = new Vector3(hori * speed, velocity, vert * speed);
        cont.Move(transform.rotation * move * Time.deltaTime);
        
    }
}
