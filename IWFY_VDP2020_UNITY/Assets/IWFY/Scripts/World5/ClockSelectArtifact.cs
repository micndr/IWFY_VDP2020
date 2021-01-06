using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSelectArtifact : IwfyClickableObjectNoPopup {

    public bool OpenSelection = false;
    public int value = 0;
    private GameObject _pointer;

    private void Start()
    {
        _pointer = GameObject.FindGameObjectWithTag("IwfyReticlePointer");
    }

    public override void OnPointerClick () {
        base.OnPointerClick();
        transform.parent.GetComponentInChildren<ClockSelect>().Select(gameObject);
        _pointer.SendMessage("OnClickableObjectExit", SendMessageOptions.DontRequireReceiver);
    }
}
