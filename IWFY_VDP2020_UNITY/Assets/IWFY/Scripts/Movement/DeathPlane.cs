using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            other.transform.position = new Vector3(0, 110, 0);
            other.transform.rotation = Quaternion.identity;
        } 
    }
}
