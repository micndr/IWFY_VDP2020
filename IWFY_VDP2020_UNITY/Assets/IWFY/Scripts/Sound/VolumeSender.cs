using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSender : MonoBehaviour
{
    public Slider volume;
    public GlobalState globalState;
    public GameObject soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Slider>();
        globalState = FindObjectOfType<GlobalState>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }
    
    public void OnValueChanged()
    {
        globalState.OnVolumeChanged(volume.value);
        soundManager.SendMessage("SetMasterVolume", (float) (20.0 * Math.Log10(volume.value)));
    }
}
