using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalAnimation : MonoBehaviour {

    public GameObject portalFx;

    float timer;
    float duration;

    void Start() {
        
    }

    void Pulse () {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        for (int i=0; i<6; i++) {
            GameObject obj = GameObject.Instantiate(portalFx);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            float scale = 1 - (1 / 6) * i;
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    void Update() {
        if (timer < Time.time) {
            duration = Random.Range(1, 2);
            timer = Time.time + duration;
            Pulse();
        }

        float anim =  1 - (timer - Time.time) / duration; // from 0 to 1
        foreach (Transform child in transform) {
            gameObject.transform.localPosition += transform.forward * child.GetSiblingIndex() / 1000f;
            gameObject.transform.localScale -= Vector3.one * child.GetSiblingIndex() / 1000f;
        }
    }
}
