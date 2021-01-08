using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalAnimation : MonoBehaviour {

    public GameObject portalFx;

    List<GameObject> portalFxs = new List<GameObject>();

    float timer;
    float duration;

    public int numOfParticles = 6;
    public float timeBetweenPulses = 1;
    public float speedForwardMovement = 1;
    public float speedScaling = 1;
    public float speedTransparency = 1;

    void Start() {
        timer = 0;
    }

    void Pulse() {
        foreach (GameObject child in portalFxs) {
            Destroy(child);
        }
        portalFxs.Clear();
        for (int i = 0; i < numOfParticles; i++) {
            GameObject obj = GameObject.Instantiate(portalFx);
            obj.name = namesLimbo[Random.Range(0, namesLimbo.Length)];
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            float scale = 1 - (1.0f / numOfParticles) * i;
            scale *= 0.5f;
            obj.transform.localScale = new Vector3(scale, scale, 1);
            Renderer rend = obj.GetComponentInChildren<Renderer>();
            rend.material.color = new Color(
                rend.material.color.r,
                rend.material.color.g,
                rend.material.color.b,
                1);
            portalFxs.Add(obj);
        }
    }

    void Update() {
        if (timer < Time.time) {
            duration = Random.Range(1, 2) * 0 + timeBetweenPulses;
            timer = Time.time + duration;
            Pulse();
        }

        float anim = 1 - (timer - Time.time) / duration; // from 0 to 1
        foreach (GameObject child in portalFxs) {
            if (!child) {
                print("abort");
                portalFxs.Clear();
                return;
            }
            child.transform.localPosition += transform.forward * child.transform.GetSiblingIndex() / 10000f * speedForwardMovement;
            child.transform.localScale += Vector3.one * child.transform.GetSiblingIndex() / 2500f * speedScaling;
            Renderer rend = child.GetComponentInChildren<Renderer>();
            rend.material.color = new Color(
                rend.material.color.r,
                rend.material.color.g,
                rend.material.color.b,
                Mathf.Max(0, rend.material.color.a - 0.005f * speedTransparency));
        }
    }

    string[] namesLimbo = {
        "Omero", "Orazio",
        "Ovidio", "Lucano",
        "Elettra", "Ettore",
        "Enea", "Cesare",
        "Camilla", "Pantasilea",
        "Re Latino", "Lavinia",
        "Bruto", "Tarquinio il Superbo", "Lucrezia",
        "Giulia", "Marzia",
        "Cornelia", "Saladino",
        "Aristotele", "Socrate",
        "Platone", "Democrito",
        "Diogene il Cinico",
        "Dioscoride", "Orfeo",
        "Cicerone", "Lino",
        "Seneca", "Euclide",
        "Tolomeo", "Ippocrate",
        "Avicenna", "Galeno",
        "Averroè",
        "Virgilio", "Stasi"
    };
}
