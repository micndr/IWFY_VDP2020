using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSelectArtifact : IwfyClickableObjectNoPopup {

    public bool OpenSelection = false;
    public int value = 0;

    public override void OnPointerClick () {
        base.OnPointerClick();
        transform.parent.GetComponentInChildren<ClockSelect>().Select(gameObject);
    }
}
