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

    GameObject destination;

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
        transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNorm);
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

    void RaycastDestination () {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            destination.transform.position = hit.point;
            destination.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }

    void Start() {
        destination = GameObject.Find("destination");
        rigidbody = GetComponent<Rigidbody>();
        planet = GameObject.Find("planet").GetComponent<Rigidbody>();
        cam = transform.Find("Camera Offset").Find("Main Camera").gameObject;
        pcMapping = cam.GetComponent<PCMapping>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastDestination();
        }

        Vector3 todest = destination.transform.position - transform.position;

        // move to dest, linearly
        if (todest.sqrMagnitude > 1) {
            rigidbody.AddForce(todest.normalized * 3000f * Time.deltaTime);
        }

        pcMapping.planetSnapping = transform.rotation;
    }

    private void FixedUpdate() {
        Attract();
    }
}
