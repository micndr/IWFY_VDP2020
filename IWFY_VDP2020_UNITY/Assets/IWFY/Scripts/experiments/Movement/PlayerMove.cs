using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* movement prototype: quaternions based on planet center */

// add physics support (https://mikeloscocco.wordpress.com/2015/10/13/mario-galaxy-physics-in-unity/)
// * add forces to move player
// raycast destination
// path validation
// path truncation before obstacle
// interpolation

public class PlayerMove : MonoBehaviour {

    Rigidbody rigidbody;

    Rigidbody planet;
    Quaternion rot = Quaternion.identity;
    float mag = 20;

    GameObject cam;
    PCMapping pcMapping;

    Vector3 FindSurface (Rigidbody Planet) {
        float dist = Vector3.Distance(this.transform.position, planet.transform.position);
        Vector3 surfaceNorm = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, dist)) {
            surfaceNorm = hit.normal;
        }
        return surfaceNorm;
    }
    
    void OrientBody (Vector3 surfaceNorm) {
        transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNorm) 
            * transform.rotation;
    }

    void Attract () {
        Vector3 surfaceNorm = FindSurface(planet);
        OrientBody(surfaceNorm);
        float dist = Vector3.Distance((transform.position) + planet.transform.position,
            planet.transform.position);
        float pullForce = -9.8f * (planet.mass) / (dist*dist);

        Vector3 pullVec = transform.position - planet.transform.position;
        rigidbody.AddForce(pullVec.normalized * pullForce * Time.deltaTime);
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        planet = GameObject.Find("planet").GetComponent<Rigidbody>();
        cam = transform.Find("Camera Offset").Find("Main Camera").gameObject;
        pcMapping = cam.GetComponent<PCMapping>();
    }

    void Update() {
        /*
        Quaternion acc = Quaternion.Euler(
            Input.GetAxis("Vertical")*0.1f, 0, Input.GetAxis("Horizontal")*0.1f);
        rot *= acc;
        transform.rotation = rot;

        pcMapping.planetSnapping = rot;*/
    }

    private void FixedUpdate() {
        Attract();
    }
}
