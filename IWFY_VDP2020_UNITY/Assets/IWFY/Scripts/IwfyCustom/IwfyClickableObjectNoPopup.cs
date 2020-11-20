using System;
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
    public virtual void Start()
    {
        _reticlePointer = GameObject.FindGameObjectWithTag("IwfyReticlePointer");
    }

    public virtual void OnPointerEnter()
    {
        _reticlePointer.SendMessage("Animate");

    }
    public virtual void OnPointerClick() {
        // Jacopo -> ho messo anche qua l'attivazione dei trigger. Se si vuole si può spostare
        Triggerer triggerer = GetComponent<Triggerer>();
        if (triggerer) triggerer.Trigger();
    }


    public virtual void OnPointerExit()
    {
        _reticlePointer.SendMessage("OnClickableObjectExit");
    }

    public GameObject getReticlePointer()
    {
        return _reticlePointer;
    }
}