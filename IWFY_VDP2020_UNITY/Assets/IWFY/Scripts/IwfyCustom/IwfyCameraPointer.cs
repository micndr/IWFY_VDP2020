﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a modified version of the CameraPointer.cs found in
// Assets/Samples/Google Cardboard XR Plugin for Unity/1.3.0/Hello Cardboard/Scripts
// All due credits/copyright to Google

// This script is responsible for Raycasting what the pointer points and notify the object which it is gazing.
// It sends:
// OnPointerEnter -> when entering the object
// OnPointerExit -> when exiting the object
// OnPointerClick -> when either Cardboard Button or Mouse Left Button is pressed

public class IwfyCameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
        }
        
        if (Input.GetMouseButtonDown(0) || Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }
}