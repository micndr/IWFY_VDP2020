using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
