using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSelectArtifact : MonoBehaviour {

    public bool OpenSelection = false;
    public int value = 0;

    void OnPointerClick () {
        transform.parent.GetComponentInChildren<ClockSelect>().Select(gameObject);
    }
}
