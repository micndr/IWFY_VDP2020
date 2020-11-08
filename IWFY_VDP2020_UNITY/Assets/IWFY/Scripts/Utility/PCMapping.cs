using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script maps the movement of the mouse to the main camera movement for use with PC
// BUG: Right now, if this is active when compiling for mobile, it ihibits all "SendMessage" instructions (no pointer interaction, no touch input et cetera)
// If anyone spots the problem please fix it and update documentation :)

public class PCMapping : MonoBehaviour
{
    public Transform tramsform;
    public float speedH = 2f;
    public float speedV = 2f;
    
    public float pitch = 0f;
    public float yaw = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");
        
        transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);
    }
}
