using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoController : MonoBehaviour {

    private RawImage rawImage;
    private VideoPlayer videoPlayer;
    private float videoLenght;
    public Triggerer triggerer;
    float timer;

    bool play;
    
    void Start() {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoLenght = (float)(videoPlayer.clip.frameCount / videoPlayer.clip.frameRate);
    }

    public void Play () {
        if (!play) { play = true; return; }
        videoPlayer.Play();
        timer = Time.time;
        rawImage.color = new Color(1, 1, 1, 1);
    }

    public void Stop() {
        videoPlayer.Stop();
        triggerer.Trigger();
        rawImage.color = new Color(1, 1, 1, 0);
    }

    private void Update() {
        if (play) { Play(); play = false; }
        if (Time.time - timer > videoLenght) {
            Stop();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Stop();
        }
    }
}
