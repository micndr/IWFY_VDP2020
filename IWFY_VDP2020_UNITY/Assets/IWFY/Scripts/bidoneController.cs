using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidoneController : MonoBehaviour
{
    public void RotateBidone()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.transform.Rotate(0,0,30);
        }
    }
}
