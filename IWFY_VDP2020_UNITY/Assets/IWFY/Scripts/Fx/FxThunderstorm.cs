using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxThunderstorm : MonoBehaviour {

    public GameObject player;
    public GameObject FxThunder;

    public float spreadPlanetDegrees = 20;
    public float frequency = 0.1f;
    public float timingSpread = 2;
    float timer;

    void Start() {
        player = GameObject.Find("Player");
        timer = Time.time;
    }

    public void Strike (Vector3 pos, Vector3 head) {
        GameObject thunderObj = Instantiate(FxThunder, pos, Quaternion.identity);
        FxThunder thunder = thunderObj.GetComponent<FxThunder>();
        thunder.height = 100;
        thunder.GenerateThunder(pos, head);
    }

    void Update() {
        if (Time.time - timer > 1/frequency) {
            timer = Time.time + Random.Range(-timingSpread, timingSpread);

            Vector3 pos = player.transform.position;
            Quaternion rot = Quaternion.Euler(
                Random.Range(-spreadPlanetDegrees, spreadPlanetDegrees),
                0,
                Random.Range(-spreadPlanetDegrees, spreadPlanetDegrees));
            pos = rot * pos;
            Vector3 head = pos.normalized;
            Strike(pos, head);
        }
    }
}
