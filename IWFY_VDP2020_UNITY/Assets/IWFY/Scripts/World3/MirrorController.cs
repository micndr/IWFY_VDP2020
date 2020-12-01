using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float y_axis;
    [SerializeField] private GlobalWorld3 globe;

    void Awake()
    {
        CheckAngles(y_axis);
        CheckAngles(this.transform.rotation.y);
    }



    public void RotateMirror()
    {
        if (this.transform.rotation.y != y_axis)
        {
            Debug.Log("dentro rotate");
            this.transform.Rotate(0, 90, 0);
            //CheckAngles(this.transform.rotation.y);
            Debug.Log("l'angolo è " + transform.rotation.y);
            if (this.transform.rotation.y == y_axis)
            {
                Debug.Log("specchio corretto");
                globe.increaseMirror();
            }
        }
        else
        {
            active = true;
            globe.increaseMirror();
        }
    }

    private void CheckAngles(float angle)
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
    }
}
