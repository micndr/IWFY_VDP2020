﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes the object do a wee tiny jump.

public class SimpleJump : MonoBehaviour
{
    public Rigidbody rb;
    private bool _jump = false;

    private void FixedUpdate()
    {
        if (_jump)
        {
            _jump = false;
            rb.AddForce(0f, 5000*Time.fixedDeltaTime, 0f);
        }
    }

    public void JumpUp()
    {
        _jump = true;
    }

    public void Vibrate()
    {
        AndroidVibrationManager.Vibrate(100);
    }

    public void OnPointerClick()
    {
        Vibrate();
        JumpUp();
    }

    public void OnPointerEnter()
    {
        Debug.Log("Entered");
        
    }

    public void OnPointerExit()
    {
        Debug.Log("Exited");
    }
}
