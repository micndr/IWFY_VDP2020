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
    
    void Start() {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoLenght = (float)(videoPlayer.clip.frameCount / videoPlayer.clip.frameRate);
    }

    public void Play () {
        timer = Time.time;
        rawImage.color = new Color(1, 1, 1, 1);
    }

    private void Update() {
        if (Time.time - timer > videoLenght) {
            triggerer.Trigger();
            rawImage.color = new Color(1, 1, 1, 0);
        }
    }
}
