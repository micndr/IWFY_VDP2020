﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// This script contains the necessary code to make the reticle pointer react to a clickable object.

// Receives the messages from the IwfyCameraPointer and bounces the signal back to the IwfyReticlePointer
// (of which it needs to be linked to in the unity editor)

public class IwfyClickableObjectNoPopup : MonoBehaviour
{
    private GameObject _reticlePointer;
    [SerializeField] private bool doesNotDisappearWhenClicked = false;
    public virtual void Start()
    {
        _reticlePointer = GameObject.FindGameObjectWithTag("IwfyReticlePointer");
    }

    public virtual void OnPointerEnter()
    {
        if (!enabled) return;
        if(_reticlePointer != null)
            _reticlePointer.SendMessage("Animate");
        //Debug.Log("Mouse enter");
        if (GetComponent<Outline>() != null)
        {
            //Debug.Log("Component activated");
            GetComponent<Outline>().enabled = true;
        }

        if (transform.parent && this.transform.parent.gameObject.GetComponent<Outline>() != null)
        {
            this.transform.parent.gameObject.GetComponent<Outline>().enabled = true;
        }
    }

    public virtual void OnPointerExit() {
        if (!enabled) return;
        if (_reticlePointer != null)
            _reticlePointer.SendMessage("OnClickableObjectExit");
        //Debug.Log("Mouse exit");
        if (GetComponent<Outline>() != null)
        {
            //Debug.Log("Component disabled");
            GetComponent<Outline>().enabled = false;
        }
        
        if (transform.parent && this.transform.parent.gameObject.GetComponent<Outline>() != null)
        {
            this.transform.parent.gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    public virtual void OnPointerClick() {
        if (!enabled) return;
        // Jacopo -> ho messo anche qua l'attivazione dei trigger. Se si vuole si può spostare
        Triggerer triggerer = GetComponent<Triggerer>();
        if (triggerer)
        {
            triggerer.Trigger();
            
            if (!doesNotDisappearWhenClicked) _reticlePointer.SendMessage("OnClickableObjectExit", SendMessageOptions.DontRequireReceiver);
            return;
        }

        if (transform.parent.gameObject.GetComponent<Triggerer>() != null)
        {
            triggerer = transform.parent.gameObject.GetComponent<Triggerer>();
            if(triggerer) triggerer.Trigger();
            
            if (!doesNotDisappearWhenClicked) _reticlePointer.SendMessage("OnClickableObjectExit", SendMessageOptions.DontRequireReceiver);
            return;
        }
    }



    public GameObject getReticlePointer()
    {
        return _reticlePointer;
    }
}