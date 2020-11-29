using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingRadial : MonoBehaviour {

    public float amplitude = 0.001f;
    public float speed = 1f;

    void Update() {
        transform.position += transform.position.normalized * Mathf.Sin(Time.time * speed) * amplitude;
    }
}
