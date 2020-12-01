using UnityEngine;
using System.Collections;

// Sebastian Lague https://github.com/SebLague/Spherical-Gravity

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

    GravityAttractor planet;
    Rigidbody rigidbody;

    void Awake() {
        Debug.Log("here");
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        Debug.Log(GameObject.FindGameObjectWithTag("Planet"));
        Debug.Log(GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>());
        Debug.Log("here");
        rigidbody = GetComponent<Rigidbody>();

        // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        // Allow this body to be influenced by planet's gravity
        planet.Attract(rigidbody);
    }
}