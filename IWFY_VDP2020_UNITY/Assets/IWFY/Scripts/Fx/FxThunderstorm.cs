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

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = Time.time;

        GameObject globalStateObj = GameObject.Find("GlobalState");
        if (globalStateObj) globalState = globalStateObj.GetComponent<GlobalState>();
    }

    public void Strike(Vector3 pos, Vector3 head) {
        GameObject thunderObj = Instantiate(FxThunder, pos, Quaternion.identity);
        FxThunder thunder = thunderObj.GetComponent<FxThunder>();
        thunder.height = 100;
        thunder.GenerateThunder(pos, head);

        GameObject audioObj = Instantiate(FxAudio, pos, Quaternion.identity);
        AudioSource audio = audioObj.GetComponent<AudioSource>();
        Destroy(audioObj, 8);
        if (globalState) {
            audio.volume = globalState.globalVolume;
        }
        audio.pitch = Random.Range(0.5f, 1.5f);
        audio.Play();
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
