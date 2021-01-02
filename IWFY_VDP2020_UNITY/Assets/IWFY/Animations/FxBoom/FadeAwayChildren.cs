using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayChildren : MonoBehaviour {

    public float duration;

    public float timer = float.MaxValue;
    List<MeshRenderer> mats = new List<MeshRenderer>();

    void Start() {
        foreach (Transform child in transform) {
            mats.Add(child.GetComponent<MeshRenderer>());
        }
    }

    private void OnEnable() {
        timer = Time.time;
    }

    void Update() {
        if (timer > Time.time) { timer = Time.time; }

        float t = (Time.time - timer) / duration;
        foreach(MeshRenderer m in mats) {
            m.material.color = new Color(
                m.material.color.r,
                m.material.color.g,
                m.material.color.b,
                Mathf.Lerp(1, 0, t));
        }
    }
}
