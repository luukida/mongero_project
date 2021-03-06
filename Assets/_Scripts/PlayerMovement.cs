﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject ("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return instance;
        }
    }
    private static PlayerMovement instance;

    Rigidbody rb;
    public float moveSpeed = 10f;
    public Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Debug.Log ("moveHorizontal : " + moveHorizontal + " / moveVertical : " + moveVertical);

        rb.velocity = new Vector3 (moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);
        
        if (JoystickMovement.Instance.joyVec.x != 0 || JoystickMovement.Instance.joyVec.z != 0)
        {
            rb.velocity = new Vector3 (JoystickMovement.Instance.joyVec.x, rb.velocity.y, JoystickMovement.Instance.joyVec.y) * moveSpeed;
            rb.rotation = Quaternion.LookRotation ( new Vector3 ( JoystickMovement.Instance.joyVec.x, 0, JoystickMovement.Instance.joyVec.y ) );
        }
    }
}
