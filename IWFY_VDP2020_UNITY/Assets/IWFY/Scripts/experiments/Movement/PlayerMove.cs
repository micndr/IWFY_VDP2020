using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* movement prototype: quaternions based on planet center */

public class PlayerMove : MonoBehaviour {

    GameObject planet;
    Quaternion rot = Quaternion.identity;
    float mag = 20;

    GameObject cam;
    PCMapping pcMapping;

    void Start() {
        planet = GameObject.Find("planet");
        cam = transform.Find("Camera Offset").Find("Main Camera").gameObject;
        pcMapping = cam.GetComponent<PCMapping>();
    }

    void Update() {
        Quaternion acc = Quaternion.Euler(Input.GetAxis("Vertical")*0.1f, 0, Input.GetAxis("Horizontal")*0.1f);
        rot *= acc;
        transform.position = rot * Vector3.up * mag;
        transform.rotation = rot;

        pcMapping.planetSnapping = rot;
    }
}
