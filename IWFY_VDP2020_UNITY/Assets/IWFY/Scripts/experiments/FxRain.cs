using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxRain : MonoBehaviour {

    ParticleSystem particles;
    ParticleSystem.ShapeModule shape;

    GameObject player;

    public float rainHeight = 180;

    void Start() {
        particles = transform.Find("rainParticles").GetComponent<ParticleSystem>();
        shape = particles.shape;

        player = GameObject.Find("XRRig");
    }

    void Update() {
        Quaternion rot = Quaternion.FromToRotation(
            Vector3.up,
            player.transform.position.normalized);

        Vector3 pos = rot * new Vector3(0, rainHeight, 0);
        shape.position = new Vector3(pos.x, -pos.z, pos.y);

        Vector3 rotDeg = rot.eulerAngles;
        shape.rotation = new Vector3(
            rotDeg.x,
            -rotDeg.z,
            rotDeg.y);
    }
}
