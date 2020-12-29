using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    private bool active = false;
    private float velocity;
    private Vector3 currentAngles;
    private Vector3 toBeAngles;
    [SerializeField] private float y_axis;
    [SerializeField] private GlobalWorld3 globe;
    
    public void RotateMirror()
    {
        if (this.transform.rotation.y != y_axis)
        {
            this.transform.Rotate(0, 15, 0);
            //CheckAngles(this.transform.rotation.y);
        //     if (Mathf.Approximately(this.transform.rotation.y, y_axis))
        //     {
        //         globe.increaseMirror();
        //         Destroy(this.GetComponent<BoxCollider>());
        //         Destroy(this);
        //     }
        // }
        // else
        // {
        //     active = true;
        //     globe.increaseMirror();
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKey("R") && Input.GetKey(KeyCode.Mouse1))
    //     {
    //         currentAngles = this.transform.eulerAngles;
    //         toBeAngles = currentAngles + new Vector3(1, 1, 1) * Time.deltaTime * velocity;
    //         transform.eulerAngles = toBeAngles;
    //     }
    // }

    /*private void CheckAngles(float angle)
    {
        if (angle < 0)
        {
            angle = angle + 360f;
            CheckAngles(angle);
            Debug.Log("primo if");
        }

        if (angle > 360f)
        {
            angle = angle % 360f;

        }
    }*/
}
