using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // when the Player tagged gameobject collides, reset position and rotation.
            other.transform.position = GameObject.Find("PlayerPortalSpawnpoint").transform.position;
            other.transform.rotation = Quaternion.identity;
        } 
    }
}
