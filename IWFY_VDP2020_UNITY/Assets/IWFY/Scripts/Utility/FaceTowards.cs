using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates around local up vector facing target transform
/// </summary>
public class FaceTowards : MonoBehaviour {

    public Transform target;
    public bool targetIsPlayer = true;

    public float rotationSpeed = 2f;

    void Start() {
        if (targetIsPlayer) { target = GameObject.Find("Player").transform; }
    }

    void Update() {
        var rotate = Quaternion.LookRotation(transform.position - target.transform.position);
        if (transform.parent) {
            rotate *= Quaternion.Euler(0, -transform.parent.localEulerAngles.y, 0);
        }
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotate, rotationSpeed);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
