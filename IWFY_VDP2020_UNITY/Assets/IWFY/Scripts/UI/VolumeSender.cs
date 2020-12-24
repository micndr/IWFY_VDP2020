using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSender : MonoBehaviour
{
    private Slider volume;
    private GlobalState globalState;
    private GameObject _audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Slider>();
        globalState = FindObjectOfType<GlobalState>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        
        globalState.OnVolumeChanged((float) (20.0 * Math.Log10(volume.value)));    
    }
    
    public void OnValueChanged()
    {
        _audioManager.SendMessage("PlayUI", 1);
        globalState.OnVolumeChanged((float) (20.0 * Math.Log10(volume.value)));    
    }
}
