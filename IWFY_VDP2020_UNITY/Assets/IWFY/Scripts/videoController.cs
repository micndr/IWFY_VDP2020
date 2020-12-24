using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// renders on rawimage the video
public class videoController : MonoBehaviour {

    private Image rawImageBack;
    private RawImage rawImage;
    private VideoPlayer videoPlayer;
    private float videoLenght;
    public Triggerer triggerer;
    float timer;

    bool play;

    bool isplaying = false;

    private AudioSource _worldAudioSource;

    public float delay = 0;
    bool sched = false;
    float schedtime = 0;
    
    void Start() {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        rawImageBack = GameObject.Find("RawImageBack").GetComponent<Image>();
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoLenght = (float)(videoPlayer.clip.frameCount / videoPlayer.clip.frameRate);
    }

    public void Play () {
        if (!play) { play = true; return; }
        if (delay > 0) {
            schedtime = Time.time + delay;
            sched = true;
            delay = 0;
            return;
        }
        videoPlayer.Play();
        timer = Time.time;
        rawImage.color = new Color(1, 1, 1, 1);
        rawImageBack.color = new Color(0, 0, 0, 1);
        
        isplaying = true;
    }

    public void Stop() {
        videoPlayer.Stop();
        triggerer.Trigger();
        rawImage.color = new Color(1, 1, 1, 0);
        rawImageBack.color = new Color(0, 0, 0, 0);
    }

    private void Update() {
        if (play) { Play(); play = false; }
        if (isplaying && Time.time - timer > videoLenght) {
            Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Stop();
        }

        if (sched && schedtime < Time.time) {
            Play();
            sched = false;
        }
    }
}
