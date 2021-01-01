using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalAnimation : MonoBehaviour {

    public GameObject portalFx;

    List<GameObject> portalFxs = new List<GameObject>();

    float timer;
    float duration;

    void Start() {
        
    }

    void Pulse () {
        foreach(GameObject child in portalFxs) {
            Destroy(child);
        }
        portalFxs.Clear();
        for (int i=0; i<6; i++) {
            GameObject obj = GameObject.Instantiate(portalFx);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            float scale = 1 - (1.0f / 6) * i;
            scale *= 0.5f;
            obj.transform.localScale = new Vector3(scale, scale, scale);
            Renderer rend = obj.GetComponentInChildren<Renderer>();
            rend.material.color = new Color(
                rend.material.color.r,
                rend.material.color.g,
                rend.material.color.b,
                Mathf.Max(0, scale));
            portalFxs.Add(obj);
        }
    }

    void Update() {
        if (timer < Time.time) {
            duration = Random.Range(1, 2) * 0 + 1;
            timer = Time.time + duration;
            Pulse();
            print("pulse");
        }

        float anim =  1 - (timer - Time.time) / duration; // from 0 to 1
        foreach (GameObject child in portalFxs) {
            child.transform.localPosition += transform.forward * child.transform.GetSiblingIndex() / 10000f;
            child.transform.localScale += Vector3.one * child.transform.GetSiblingIndex() / 2500f;
            Renderer rend = child.GetComponentInChildren<Renderer>();
            rend.material.color = new Color(
                rend.material.color.r,
                rend.material.color.g,
                rend.material.color.b,
                Mathf.Max(0, rend.material.color.a - 0.005f));
        }
    }
}
