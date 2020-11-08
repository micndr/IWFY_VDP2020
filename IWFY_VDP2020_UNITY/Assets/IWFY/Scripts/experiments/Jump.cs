using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource red;
    public AudioSource black;
    public Material material;
    private bool _clicked = false;

    private bool _jump = false;
    // Start is called before the first frame update
    void Start()
    {
        red.mute = false;
        black.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (material.color == Color.black) _clicked = true;
        else _clicked = false;
    }

    private void FixedUpdate()
    {
        if (_jump)
        {
            _jump = false;
            rb.AddForce(0f, 5000*Time.fixedDeltaTime, 0f);
        }
    }

    public void ToggleColorAndAudio()
    {
        if (_clicked)
        {
            material.color = Color.red;
            red.mute = false;
            black.mute = true;
            Debug.Log("Activated red");
        }
        else
        {
            material.color = Color.black;
            black.mute = false;
            red.mute = true;
            Debug.Log("Activated black");
        }
    }

    public void JumpUp()
    {
        _jump = true;
    }

    public void Vibrate()
    {
        // AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        // AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity"); 
        // AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        // vibrator.Call("vibrate", 1000);
        AndroidVibrationManager.Vibrate(100);
    }

    public void OnPointerClick()
    {
        Vibrate();
        ToggleColorAndAudio();
        JumpUp();
    }

    public void OnPointerEnter()
    {
        Debug.Log("Entered");
        
    }

    public void OnPointerExit()
    {
        Debug.Log("Exited");
    }
}
