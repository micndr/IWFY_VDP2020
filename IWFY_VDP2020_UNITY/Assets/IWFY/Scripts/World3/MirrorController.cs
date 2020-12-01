using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float y_axis;
    [SerializeField] private GlobalWorld3 globe;
    
    public void RotateMirror()
    {
        if (this.transform.rotation.y != y_axis)
        {
            this.transform.Rotate(0, 90, 0);
            //CheckAngles(this.transform.rotation.y);
            if (Mathf.Approximately(this.transform.rotation.y, y_axis))
            {
                globe.increaseMirror();
            }
        }
        else
        {
            active = true;
            globe.increaseMirror();
        }
    }

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
