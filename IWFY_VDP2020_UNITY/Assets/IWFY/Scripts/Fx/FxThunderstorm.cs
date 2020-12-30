using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxThunderstorm : MonoBehaviour {

    public GameObject player;
    public GameObject FxThunder;
    public GameObject FxAudio;
    public GlobalState globalState;

    public float spreadPlanetDegrees = 20;
    public float frequency = 0.1f;
    public float timingSpread = 2;
    float timer;

    AudioManager audioManager;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = Time.time;

        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Strike(Vector3 pos, Vector3 head) {
        GameObject thunderObj = Instantiate(FxThunder, pos, Quaternion.identity);
        FxThunder thunder = thunderObj.GetComponent<FxThunder>();
        thunder.height = 100;
        thunder.GenerateThunder(pos, head);

        audioManager.PlayAmbient(1);
    }

    void Update() {
        if (Time.time - timer > 1/frequency) {
            timer = Time.time + Random.Range(-timingSpread, timingSpread);

            Vector3 pos = player.transform.position;
            /*
            Quaternion rot = Quaternion.Euler(
                Random.Range(-spreadPlanetDegrees, spreadPlanetDegrees),
                0,
                Random.Range(-spreadPlanetDegrees, spreadPlanetDegrees));
            */
            pos += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Vector3 head = Vector3.up;
            Strike(pos, head);
        }
    }
}
