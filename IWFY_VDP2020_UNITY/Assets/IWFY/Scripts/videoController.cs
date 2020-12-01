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

    private AudioSource _worldAudioSource;
    
    void Start() {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoLenght = (float)(videoPlayer.clip.frameCount / videoPlayer.clip.frameRate);
        _worldAudioSource = GameObject.Find("PlanetPrefab").GetComponent<AudioSource>();
        Debug.Log(_worldAudioSource);
    }

    public void Play () {
        if (!play) { play = true; return; }
        videoPlayer.Play();
        timer = Time.time;
        rawImage.color = new Color(1, 1, 1, 1);
        
        // Mute ost
        _worldAudioSource.Stop();
    }

    public void Stop() {
        videoPlayer.Stop();
        triggerer.Trigger();
        rawImage.color = new Color(1, 1, 1, 0);
        
        // Restart ost
        _worldAudioSource.Play();
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
