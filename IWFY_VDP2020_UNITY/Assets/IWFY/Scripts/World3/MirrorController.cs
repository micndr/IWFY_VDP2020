using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float y_axis;

    void Awake()
    {
        CheckAngles(y_axis);
        CheckAngles(this.transform.rotation.y);
    }

    

    public void RotateMirror()
    {
        if (this.transform.rotation.y != y_axis)
        {
            this.transform.Rotate(0, 90, 0);
            CheckAngles(this.transform.rotation.y);
        }
        else
        {
            active = true;
            GlobalWorld3.increaseMirror();
        }
    }

    private void CheckAngles(float angle)
    {
        if (angle < 0)
        {
            angle = angle + 360f;
            CheckAngles(angle);
        }

        if (angle > 360f)
        {
            angle = angle % 360f;
        }
    }
}
