using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _master;

    public void SetMasterVolume(float vol)
    {
        _master.SetFloat("MasterVolume", vol);
    }

    public float GetMasterVolume()
    {
        float vol;
        _master.GetFloat("MasterVolume", out vol);
        return vol;
    }

}
